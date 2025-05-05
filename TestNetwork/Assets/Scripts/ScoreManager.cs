using Mirror;
using UnityEngine;

public class ScoreManager : NetworkBehaviour
{
    public static ScoreManager instance;

    [SyncVar(hook = nameof(OnScoreChanged))]
    private int sharedScore = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int value)
    {
        if (!isServer) return;

        sharedScore += value;
    }

    void OnScoreChanged(int oldScore, int newScore)
    {
        ScoreUI.instance.UpdateScoreText(newScore);
    }
}
