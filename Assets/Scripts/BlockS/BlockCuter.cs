using Unity.VisualScripting;
using UnityEngine;

public class BlockCuter : MonoBehaviour
{
    [SerializeField] private CutBlocksPool cutBlocksPool;

    public bool TryCutBlock(BlockMovement currentBlock, BlockMovement previousBlock)
    {
        return CutBlock(currentBlock, previousBlock);
    }

    private bool CutBlock(BlockMovement currentBlock, BlockMovement previousBlock)
    {
        bool onX = currentBlock.GetMovingOnX();

        Transform currentTransform = currentBlock.transform;
        Transform prevTransform = previousBlock.transform;

        Vector3 currentScale = currentTransform.localScale;
        Vector3 prevScale = prevTransform.localScale;

        float delta;
        float overlap;

        if (onX)
        {
            delta = currentTransform.position.x - prevTransform.position.x;
            overlap = prevScale.x - Mathf.Abs(delta);

            if (overlap <= 0f)
            {
                FullFoll(currentBlock);
                return false;
            }

            float newXScale = overlap;
            currentScale.x = newXScale;
            currentTransform.localScale = currentScale;

            float newXPos = prevTransform.position.x + delta / 2f;
            currentTransform.position = new Vector3(newXPos, currentTransform.position.y, currentTransform.position.z);

            SpawnFallingPiece(delta, overlap, prevScale.x, currentTransform, onX);
        }
        else
        {
            delta = currentTransform.position.z - prevTransform.position.z;
            overlap = prevScale.z - Mathf.Abs(delta);

            if (overlap <= 0f)
            {
                FullFoll(currentBlock);
                return false;
            }

            float newZScale = overlap;
            currentScale.z = newZScale;
            currentTransform.localScale = currentScale;

            float newZPos = prevTransform.position.z + delta / 2f;
            currentTransform.position = new Vector3(currentTransform.position.x, currentTransform.position.y, newZPos);

            SpawnFallingPiece(delta, overlap, prevScale.z, currentTransform, onX);
        }

        return true;
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
}

