using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI instance;
    public Text scoreText;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
