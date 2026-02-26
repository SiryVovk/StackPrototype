using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private float spawnBoundary = 7f;

    private int blockCounter = 0;

    public BlockMovement CreateNewBlock(BlockMovement prevBlock)
    {
        Vector3 spawnPosition = Vector3.zero;

        if (prevBlock == null)
        {
            spawnPosition = new Vector3(spawnBoundary, 0, 0);
        }
        else
        {
            Vector3 prevPos = prevBlock.transform.position;
            Vector3 prevScale = prevBlock.transform.localScale;

            bool moveOnX = !prevBlock.GetMovingOnX();

            if (moveOnX)
            {
                spawnPosition = new Vector3(
                    spawnBoundary * Mathf.Sign(spawnBoundary),
                    prevPos.y + 1f,
                    prevPos.z
                );
            }
            else
            {
                spawnPosition = new Vector3(
                    prevPos.x,
                    prevPos.y + 1f,
                    spawnBoundary * Mathf.Sign(spawnBoundary)
                );
            }
        }

        BlockMovement newBlock = Instantiate(blockPrefab, spawnPosition, Quaternion.identity).GetComponent<BlockMovement>();
        blockCounter++;

        if(prevBlock != null && prevBlock.GetMovingOnX())
        {
            newBlock.SetMovingOnX(false);
        }

        if (prevBlock != null)
        {
            newBlock.gameObject.transform.localScale = prevBlock.transform.localScale;
        }

        prevBlock = newBlock;

        return newBlock;
    }

}
