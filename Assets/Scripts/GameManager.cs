using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Events
    public Action OnGameStart;
    public Action OnGameOver;

    [Header("References")]
    [SerializeField] private PlayersInput input;
    [SerializeField] private BlockSpawner blockSpawner;
    [SerializeField] private BlockCuter blockCuter;
    [SerializeField] private CameraControl cameraControl;
    [SerializeField] private ScoreSystem scoreSystem;

    [Header("Blocks")]
    [SerializeField] private BlockMovement firstBlock;

    // Runtime block state
    private BlockMovement currentBlock;
    private BlockMovement prevBlock;

    // Game state (runtime)
    private bool isGameStart = false;
    private bool isGameOver = false;

    private int counter = 0;

    private void Awake()
    {
        input.OnTouch += HandleInoput;
    }

    private void HandleInoput()
    {
        if (!isGameStart)
        {
            isGameStart = true;
            GameStart();
            return;
        }

        if (isGameOver)
        {
            RestartGame();
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
        scoreSystem.AddScore();
        counter++;
    }

    private void GameStart()
    {
        currentBlock = blockSpawner.CreateNewBlock(prevBlock);
        currentBlock.StartMoving();
        prevBlock = firstBlock;
        OnGameStart?.Invoke();
    }

    private void GameOver()
    {
        blockSpawner.enabled = false;
        isGameOver = true;
        counter++;
        cameraControl.GameOverCameraMove(counter);
        OnGameOver?.Invoke();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
