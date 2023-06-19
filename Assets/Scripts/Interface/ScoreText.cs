using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    [SerializeField] private Score playerScore;
    
    void Start()
    {
        playerScore.ScoreChange += PlayerScore_OnScoreChange;
        scoreText.text = "0";
    }

    private void PlayerScore_OnScoreChange(object sender, System.EventArgs e)
    {
        int points = playerScore.GetScore();
        scoreText.text = points.ToString();
    }
}
