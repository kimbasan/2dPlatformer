using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int score;

    public event EventHandler ScoreChange;

    public void AddScore(int points)
    {
        score += points;
        ScoreChange(this, EventArgs.Empty);
    }

    public int GetScore() { return score; }
}
