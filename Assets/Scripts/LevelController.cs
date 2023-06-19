using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private static readonly int MENU_IND = 0;
    private static readonly int END_IND = 5;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private Text scorePointsText;
    [SerializeField] private GameObject nextLevelButton;

    [Header("External")]
    [SerializeField] private Score playerScore;
    [SerializeField] private BossDeath bossDeath;
    

    private void Awake()
    {
        if (bossDeath!= null)
        {
            bossDeath.BossDied += BossDeath_BossDied;
        }
    }

    private void BossDeath_BossDied(object sender, System.EventArgs e)
    {
        ShowScoreScreen();
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel <= END_IND) {
            SceneManager.LoadScene(nextLevel);
        }
        Time.timeScale = 1f;
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0f;
        } else
        {
            Time.timeScale = 1f;
        }
        pausePanel.SetActive(pause);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(MENU_IND);
    }

    public void ShowScoreScreen()
    {
        Time.timeScale = 0f;
        scorePointsText.text = playerScore.GetScore().ToString();

        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel > END_IND)
        {
            nextLevelButton.SetActive(false);
        }
        scorePanel.SetActive(true);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
