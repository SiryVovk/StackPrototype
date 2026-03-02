using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockCuter : MonoBehaviour
{
    [SerializeField] private CutBlocksPool cutBlocksPool;

    [SerializeField] private float perfectCutPercent = 95;
    [SerializeField] private float loseCutPercent = 5;

    public bool TryCutBlock(BlockMovement currentBlock, BlockMovement previousBlock)
    {
        return CutBlock(currentBlock, previousBlock);
    }

    private bool CutBlock(BlockMovement currentBlock, BlockMovement previousBlock)
    {
        bool onX = currentBlock.GetMovingOnX();

        Transform currentTransform = currentBlock.transform;
        Transform prevTransform = previousBlock.transform;


        float delta = GetDelta(onX, currentTransform, prevTransform);
        float prevSize = GetAxisSize(prevTransform.localScale, onX);

        float overlap = prevSize - MathF.Abs(delta);
        float overlapPercent = (overlap / prevSize) * 100;

        if (overlapPercent >= perfectCutPercent)
        {
            PerfectSnap(currentTransform, prevTransform, onX);
            return true;
        }
        else if (overlapPercent <= loseCutPercent)
        {
            FullFoll(currentBlock);
            return false;
        }
        else
        {
            ApplyCut(currentTransform, prevTransform, delta, overlap, prevSize, onX);
        }

        return true;

        //    float newXScale = overlap;
        //    currentScale.x = newXScale;
        //    currentTransform.localScale = currentScale;

        //    float newXPos = prevTransform.position.x + delta / 2f;
        //    currentTransform.position = new Vector3(newXPos, currentTransform.position.y, currentTransform.position.z);

        //    SpawnFallingPiece(delta, overlap, prevScale.x, currentTransform, onX);
        //}
        //else
        //{


        //    float newZScale = overlap;
        //    currentScale.z = newZScale;
        //    currentTransform.localScale = currentScale;

        //    float newZPos = prevTransform.position.z + delta / 2f;
        //    currentTransform.position = new Vector3(currentTransform.position.x, currentTransform.position.y, newZPos);

        //    SpawnFallingPiece(delta, overlap, prevScale.z, currentTransform, onX);
        //}
    }


    private float GetDelta(bool onX, Transform currentBlock, Transform previousBlock)
    {
        return onX ? currentBlock.position.x - previousBlock.position.x : currentBlock.position.z - previousBlock.position.z;
    }
    private float GetAxisSize(Vector3 localScale, bool onX)
    {
        return onX ? localScale.x : localScale.z;
    }

    private void SpawnFallingPiece(float delta, float overlap, float prevSize, Transform current, bool onX)
    {
        float fallingSize = prevSize - overlap;
        if (fallingSize <= 0f) return;

        Vector3 fallingScale = current.localScale;
        Vector3 fallingPos = current.position;

        if (onX)
        {
            fallingScale.x = fallingSize;

            float direction = Mathf.Sign(delta);
            fallingPos.x += (overlap / 2f + fallingSize / 2f) * direction;
        }
        else
        {
            fallingScale.z = fallingSize;

            float direction = Mathf.Sign(delta);
            fallingPos.z += (overlap / 2f + fallingSize / 2f) * direction;
        }

        SelfRelese fallingBlock = cutBlocksPool.Get();
        fallingBlock.gameObject.transform.localScale = fallingScale;
        fallingBlock.gameObject.transform.position = fallingPos;

        Rigidbody rb = fallingBlock.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void PerfectSnap(Transform currentTransform, Transform previousTransform, bool onX)
    {
        currentTransform.position = new Vector3(
            onX ? currentTransform.position.x : currentTransform.position.x,
            currentTransform.position.y,
            onX ? currentTransform.position.z : previousTransform.position.z);

        currentTransform.localScale = new Vector3(previousTransform.localScale.x, currentTransform.localScale.y,previousTransform.localScale.z);
    }

    private void FullFoll(BlockMovement currentBlock)
    {
        SelfRelese fallingBlock = cutBlocksPool.Get();
        fallingBlock.gameObject.transform.localScale = currentBlock.gameObject.transform.localScale;
        fallingBlock.gameObject.transform.position = currentBlock.gameObject.transform.position;

        Rigidbody rb = fallingBlock.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;


        Destroy(currentBlock.gameObject);
    }

    private void ApplyCut(Transform current, Transform previous, float delta, float overlap, float prevSize, bool onX)
    {
        Vector3 scale = current.localScale;

        if (onX)
        {
            scale.x = overlap;
        }
        else
        {
            scale.z = overlap;
        }
        current.localScale = scale;

        Vector3 position = current.position;

        if (onX)
        {
            position.x = previous.position.x + delta / 2f;
        }
        else
        {
            position.z = previous.position.z + delta / 2f;
        }

        current.position = position;

        SpawnFallingPiece(delta, overlap, prevSize, current, onX);
    }
}

