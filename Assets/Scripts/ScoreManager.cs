using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;  // Reference to the Text component
    private int score;      // Variable to store the score

    void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    // Method to update the score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Method to reset the score
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    // Method to update the score text
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
