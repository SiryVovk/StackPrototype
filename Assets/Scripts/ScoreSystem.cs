using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public Action<int> onScoreChange;

    private int currScore = 0;

    public void AddScore()
    {
        currScore++;
        onScoreChange?.Invoke(currScore);
    }
}
