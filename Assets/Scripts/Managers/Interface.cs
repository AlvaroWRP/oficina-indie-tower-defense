using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public static Interface Instance { get; private set; }
    public TextMeshProUGUI healthCounter;
    public TextMeshProUGUI goldCounter;
    public TextMeshProUGUI waveCounter;
    public GameObject gameOverScreen;
    public GameObject victoryScreen;
    public int currentHealth;
    public Button startWaveButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        healthCounter.text = $"Health: {currentHealth}";
        UpdateWave();
    }

    public void RemoveHealth(int hp)
    {
        currentHealth -= hp;
        healthCounter.text = $"Health: {currentHealth}";

        if (currentHealth <= 0)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void UpdateWave()
    {
        waveCounter.text = $"Wave {Waves.Instance.currentWave + 1}";
    }

    public void HideWaveButton()
    {
        startWaveButton.gameObject.SetActive(false);
    }

    public void ShowWaveButton()
    {
        startWaveButton.gameObject.SetActive(true);
    }
}
