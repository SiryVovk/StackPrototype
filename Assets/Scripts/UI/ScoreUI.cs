using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        scoreSystem.onScoreChange += UpdateScore;
    }

    private void OnDisable()
    {
        scoreSystem.onScoreChange -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString() + 'm';
    }
}
