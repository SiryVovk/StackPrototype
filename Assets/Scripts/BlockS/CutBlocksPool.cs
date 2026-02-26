using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;

public class CutBlocksPool : MonoBehaviour
{
    [SerializeField] private SelfRelese cutBlockPrefab;

    [SerializeField] private int poolSize = 5;

    private ObjectPool<SelfRelese> poolOfCutedBlocks;

    private void Awake()
    {
        poolOfCutedBlocks = new ObjectPool<SelfRelese>(
                () => CreateCutBlock(cutBlockPrefab),
                OnGetBlock,
                OnReleaseBlock,
                OnDestroyBlock,
                true,
                poolSize,
                2 * poolSize
            );
    }

    private SelfRelese CreateCutBlock(SelfRelese cutBlockPrefab)
    {
        var obj = Instantiate(cutBlockPrefab.gameObject).GetComponent<SelfRelese>();
        obj.gameObject.SetActive(false);
        obj.transform.parent = transform;
        obj.Init(this);
        return obj;
    }

    private void OnGetBlock(SelfRelese block)
    {
        block.gameObject.SetActive(true);
    }

    private void OnReleaseBlock(SelfRelese block)
    {
        block.gameObject.SetActive(false);
    }

    private void OnDestroyBlock(SelfRelese block)
    {
        Destroy(block);
    }

    public SelfRelese Get()
    {
       return poolOfCutedBlocks.Get();
    }

    public void Relese(SelfRelese block)
    {
        poolOfCutedBlocks.Release(block);
    }
}
