using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoss;
    [SerializeField] private TextMeshProUGUI scoreBoard;
    private int enemyWaveIndex = 1;
    private List<GameObject> activeEnemies = new();
    private List<Vector3> activeSpawnPos = new();
    [SerializeField] private float spawnDelay;
    public float waveBudget;
    private float initialBudget;
    private float score;
    void Start()
    {
        score = 0;
        spawnDelay = spawnDelay / GlobalSettings.modifier;
        if (GlobalSettings.difficulty > 1)
            initialBudget = waveBudget + (waveBudget * (0.5f * GlobalSettings.difficulty));
        StartCoroutine(SpawnWave());
        EnemyBase.enemyDeath += EnemyDied;
    }

    void Update()
    {
    
    }
    private void KeepScore()
    {
        score++;
        PlayerPrefs.SetFloat("score", score);
        PlayerPrefs.Save();

        scoreBoard.text = "Score: " + score;
    }
    private IEnumerator SpawnWave()
    {
        activeSpawnPos.Clear();
        activeEnemies.Clear();
        waveBudget = initialBudget * enemyWaveIndex;

        yield return new WaitForSeconds(spawnDelay);
        Debug.Log("New Wave! #" + enemyWaveIndex);

        while (waveBudget > 0)
        {
            SpawnEnemy(CreateEnemy());
        }
        enemyWaveIndex++;
    }
    private EnemyData CreateEnemy()
    {
        EnemyData newEnemy = new();

        while (waveBudget > 0)
        {
            newEnemy.spawnPos = spawnPoss[Random.Range(0, spawnPoss.Length)].position;
            newEnemy.enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            EnemyBase newEnemyBase = newEnemy.enemyPrefab.GetComponent<EnemyBase>();

            if (newEnemyBase != null)
            {
                float worth = newEnemyBase.enemyStatsTemplate.worth;
                if (waveBudget - worth >= 0)
                {
                    waveBudget -= worth;
                    return newEnemy;
                }
            }
        }
        return null;
    }
    private void SpawnEnemy(EnemyData enemy)
    {
        if (activeSpawnPos.Contains(enemy.spawnPos))
        {
            enemy.spawnPos = new Vector3(enemy.spawnPos.x + (1.5f * Random.Range(1,5)), enemy.spawnPos.y, enemy.spawnPos.z);
            SpawnEnemy(enemy);
        } else
        {
            activeSpawnPos.Add(enemy.spawnPos);

            GameObject newEnemy = Instantiate(enemy.enemyPrefab, enemy.spawnPos, Quaternion.identity);
            activeEnemies.Add(newEnemy);
        }
    }
    public void EnemyDied(EnemyBase enemy)
    {
        activeEnemies.Remove(enemy.gameObject);

        Builder.instance.AddResource(enemy.enemyStats.worth); //adding money to the builder
        Destroy(enemy.gameObject);

        KeepScore();

        if (activeEnemies.Count == 0)
        {
            StartCoroutine(SpawnWave());
        }
    }
}
public class EnemyData
{
    public Vector3 spawnPos;
    public GameObject enemyPrefab;
}