using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;


    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;


    [SerializeField] private float spawnInterval = 3f;


    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;


    public CombatManager combatManager;


    public bool isSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnCount = defaultSpawnCount;
    }

    public void stopSpawning()
    {
        isSpawning = false;
    }

    public void startSpawning()
    {
        if (spawnedEnemy.Level <= combatManager.waveNumber)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SpawnEnemies()
    {
        if (isSpawning)
        {
            if (spawnCount == 0)
            {
                spawnCount = defaultSpawnCount;
            }
            int i = spawnCount;
            while (i > 0)
            {

                Enemy enemy = Instantiate(spawnedEnemy);
                enemy.GetComponent<Enemy>().enemySpawner = this;
                enemy.GetComponent<Enemy>().combatManager = combatManager;
                --i;
                spawnCount = i;
                if (combatManager != null)
                {
                    combatManager.totalEnemies++;
                }

                yield return new WaitForSeconds(spawnInterval);
            }
        }

    }

    public void onDeath()
    {
        Debug.Log("Enemy Killed");
        // Call this method when an enemy is killed
        totalKill++;
        ++totalKillWave;
        Debug.Log(totalKillWave);

        // Check if totalKillWave has reached the minimumKillsToIncreaseSpawnCount
        if (totalKillWave == minimumKillsToIncreaseSpawnCount)
        {
            Debug.Log("Increasing spawn count");
            totalKillWave = 0; // Reset totalKillWave for the new wave
            defaultSpawnCount *= spawnCountMultiplier; // Increase defaultSpawnCount
            if (spawnCountMultiplier < 3)
                spawnCountMultiplier += multiplierIncreaseCount; // Increase the multiplier
            spawnCount = defaultSpawnCount; // Update spawnCount
        }
    }


}