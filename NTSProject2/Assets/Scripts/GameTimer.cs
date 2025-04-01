using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    private float timeRemaining = 30f;
    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timerText.text = "Time: " + Mathf.Ceil(timeRemaining);
            }
            else
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        isGameOver = true;
        timerText.text = "Time: 0";
        gameOverText.text = "Game Over!";
        gameOverText.gameObject.SetActive(true); 
        Invoke("RestartGame", 3f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}