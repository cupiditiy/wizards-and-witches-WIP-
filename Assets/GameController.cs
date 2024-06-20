using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScreenController : MonoBehaviour
{
    // Reference to the score text GameObject in GameScreen
    public GameObject scoreText;

    // Reference to the temporary text GameObject in GameScreen
    public GameObject tempText;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the texts are initially disabled
        scoreText.SetActive(false);
        tempText.SetActive(false);

        // Check if the current scene is GameScreen and enable the texts if true
        if (SceneManager.GetActiveScene().name == "GameScreen")
        {
            scoreText.SetActive(true);
            tempText.SetActive(true);
        }

        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Method called when a scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScreen")
        {
            // Enable the texts when the GameScreen scene is loaded
            scoreText.SetActive(true);
            tempText.SetActive(true);
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
