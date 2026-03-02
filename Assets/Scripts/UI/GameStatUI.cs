using Unity.VisualScripting;
using UnityEngine;

public class GameStatUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject startGameObject;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject endGameUI;

    private void OnEnable()
    {
        gameManager.OnGameStart += StartGame;
        gameManager.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        gameManager.OnGameStart -= StartGame;
        gameManager.OnGameOver -= GameOver;
    }

    private void StartGame()
    {
        startGameObject.SetActive(false);
        inGameUI.SetActive(true);
    }

    private void GameOver()
    {
        endGameUI.SetActive(true);
    }
}
