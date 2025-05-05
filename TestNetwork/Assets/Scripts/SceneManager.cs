using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Load a scene by name
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    // Reload the current scene
    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Pause the game
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    // Resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");

        // This will only work in a built game, not inside the Unity Editor.
        Application.Quit();

        // In the Editor, we can also stop play mode (only for testing).

    }
}


