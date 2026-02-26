using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayersInput input;
    [SerializeField] private BlockSpawner blockSpawner;
    [SerializeField] private BlockCuter blockCuter;
    [SerializeField] private CameraControl cameraControl;

    [SerializeField] private BlockMovement firstBlock;

    private BlockMovement currentBlock;
    private BlockMovement prevBlock;

    private bool isGameOver = false;

    private void Awake()
    {
        input.OnTouch += HandleInoput;
        currentBlock = blockSpawner.CreateNewBlock(prevBlock);
        currentBlock.StartMoving();
        prevBlock = firstBlock;
    }

    private void HandleInoput()
    {
        if (isGameOver)
        {
            return;
        }

        currentBlock.StopMoving();

        bool succesCut = blockCuter.TryCutBlock(currentBlock, prevBlock);
        if (!succesCut)
        {
            GameOver();
            return;
        }

        prevBlock = currentBlock;
        currentBlock = blockSpawner.CreateNewBlock(prevBlock);
        currentBlock.StartMoving();

        cameraControl.MoveUp(1f);
    }

    private void GameOver()
    {
        blockSpawner.enabled = false;
        isGameOver = true;
    }
}
