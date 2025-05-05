using UnityEngine;

public class HelpPanelController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject helpPanel; // Assign your Help Panel GameObject here

    private bool isPaused = false;

    public void ShowHelpPanel()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            isPaused = true;
        }
    }

    public void HideHelpPanel()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(false);
            Time.timeScale = 1f; // Resume the game
            isPaused = false;
        }
    }

    void OnDisable()
    {
        // Ensure game unpauses if object is disabled unexpectedly
        if (isPaused)
        {
            Time.timeScale = 1f;
        }
    }
}
