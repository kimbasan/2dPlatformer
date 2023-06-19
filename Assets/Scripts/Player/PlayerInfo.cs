using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Health health;
    [SerializeField] private Image healthBar;
    [Header("Score")]
    [SerializeField] private Score score;  
    [SerializeField] private Text scoreText;

    [SerializeField] private LevelController levelController;

    private void Start()
    {
        if (score!= null)
        {
            score.ScoreChange += Score_ScoreChange;
            scoreText.text = "0";

        }

        if (health!= null)
        {
            health.HealthChanged += Health_HealthChanged;
            health.Death += Health_Death;
        }
    }

    private IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(1);
        levelController.ShowGameOver();
    }

    public void ShowVictoryScreen()
    {
        levelController.ShowScoreScreen();
    }

    private void Health_Death(object sender, System.EventArgs e)
    {
        StartCoroutine(ShowGameOverScreen());
    }

    private void Health_HealthChanged(object sender, System.EventArgs e)
    {
        healthBar.fillAmount = health.GetFillAmount();
    }

    private void Score_ScoreChange(object sender, System.EventArgs e)
    {
        int points = score.GetScore();
        scoreText.text = points.ToString();
    }
}
