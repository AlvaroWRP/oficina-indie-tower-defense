using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public static Waves Instance { get; private set; }
    public int currentWave;
    public Transform spawnLocation;
    public float spawnCooldown;
    int enemiesSpawned;
    float currentSpawnCooldown;
    bool canSpawnEnemies = false;
    public int enemiesLeft;
    public List<ScriptableWaves> wavesList;

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

    void FixedUpdate()
    {
        if (canSpawnEnemies)
        {
            SpawnEnemies(wavesList[currentWave].enemy);
        }
    }

    void SpawnEnemies(GameObject enemy)
    {
        if (enemiesLeft <= 0)
        {
            canSpawnEnemies = false;
            currentWave++;

            if (currentWave >= wavesList.Count)
            {
                Interface.Instance.victoryScreen.SetActive(true);
                return;
            }
            Interface.Instance.UpdateWave();
            Interface.Instance.ShowWaveButton();
            return;
        }

        if (enemiesSpawned < wavesList[currentWave].enemiesQuantity && currentSpawnCooldown <= 0)
        {
            Instantiate(enemy, spawnLocation.position, Quaternion.identity);
            enemiesSpawned++;
            currentSpawnCooldown = spawnCooldown;
        }
        else
        {
            currentSpawnCooldown -= Time.deltaTime;
        }
    }

    public void StartWave()
    {
        enemiesSpawned = 0;
        canSpawnEnemies = true;
        enemiesLeft = wavesList[currentWave].enemiesQuantity;
        Interface.Instance.HideWaveButton();
    }
}
