using Mirror;
using TMPro;
using UnityEngine;

public class HealthManager : NetworkBehaviour
{
    public static HealthManager instance;

    [SyncVar(hook = nameof(OnHealthChanged))]
    private int health = 100;

    private TextMeshProUGUI healthText;
    [SerializeField] private Canvas gameCanvas;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        // If not manually assigned in inspector, find canvas
        if (gameCanvas == null)
            gameCanvas = FindObjectOfType<Canvas>();

        // Hide canvas until connection is ready
        if (gameCanvas != null)
            gameCanvas.enabled = false;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        // Wait briefly for UI objects to load
        Invoke(nameof(InitializeUI), 0.1f);
    }

    void InitializeUI()
    {
        healthText = GameObject.Find("HealthText")?.GetComponent<TextMeshProUGUI>();

        if (gameCanvas != null)
            gameCanvas.enabled = true;

        UpdateHealthUI();
    }

    [Server]
    public void ReduceHealth(int amount)
    {
        health -= amount;
        if (health < 0) health = 0;
    }

    void OnHealthChanged(int oldHealth, int newHealth)
    {
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = $"Health: {health}";
    }
}
