using UnityEngine;
using Mirror;
using TMPro;

public class Timer : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnTimerChanged))]
    public float timeRemaining = 120f;

    public TextMeshProUGUI timerText; // Assign in Inspector
    public GameObject gameOverPanel;  // Assign your Game Over panel in Inspector

    private bool isTimerRunning = true;

    void Start()
    {
        if (timerText == null)
        {
            timerText = GetComponentInChildren<TextMeshProUGUI>();
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // Ensure it's hidden at start
        }

        UpdateTimerUI(timeRemaining);
    }

    void Update()
    {
        if (!isServer) return;

        if (isTimerRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isTimerRunning = false;
                RpcTimerEnded();
            }
        }
    }

    void OnTimerChanged(float oldTime, float newTime)
    {
        UpdateTimerUI(newTime);
    }

    void UpdateTimerUI(float time)
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    [ClientRpc]
    void RpcTimerEnded()
    {
        Debug.Log("Timer ended!");

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f; // Optional: Pause the game on Game Over
    }
}
