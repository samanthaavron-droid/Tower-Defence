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
    [SerializeField] private float waveBudget;
    private float initialBudget;
    private float score;
    private float cheapestUnit = float.MaxValue;
    [SerializeField] private GameObject readyButton;
    [SerializeField] private TextMeshProUGUI waveNumber;
    void Start()
    {
        EnemyBase.enemyDeath -= EnemyDied;
        score = 0;
        if (GlobalSettings.GetSetDifficulty > 1)
            initialBudget = waveBudget + (waveBudget * (0.5f * GlobalSettings.GetSetDifficulty));
        else
            initialBudget = waveBudget;

        foreach (GameObject enemy in enemyPrefabs)
        {
            float worth = enemy.GetComponent<Unit>().GetStatsTemplate.worth;
            if (worth < cheapestUnit) cheapestUnit = worth;
        }
        EnemyBase.enemyDeath += EnemyDied;
    }
    public void StartWave()
    {
        if (Time.timeScale == 0) return;
        readyButton.SetActive(false);
        StartCoroutine(SpawnWave());
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

        yield return new WaitUntil(() => PauseMenu.getGameActive == true);

        waveNumber.text = "Wave #" + enemyWaveIndex;
        Color c = waveNumber.color;
        c.a = 1f;
        waveNumber.color = c;
        waveNumber.gameObject.SetActive(true);

        while (waveBudget >= cheapestUnit)
        {
            SpawnEnemy(CreateEnemy());
        }
        enemyWaveIndex++;

        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOut());
    }
    private EnemyData CreateEnemy()
    {
        EnemyData newEnemy = new();

        while (waveBudget >= cheapestUnit)
        {
            newEnemy.spawnPos = spawnPoss[Random.Range(0, spawnPoss.Length)].position;
            newEnemy.enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            EnemyBase newEnemyBase = newEnemy.enemyPrefab.GetComponent<EnemyBase>();

            if (newEnemyBase != null)
            {
                float worth = newEnemyBase.GetStatsTemplate.worth;
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

        Builder.instance.AddResource(enemy.GetStatsTemplate.worth); //adding money to the builder
        Destroy(enemy.gameObject);

        KeepScore();

        if (activeEnemies.Count == 0)
        {
            StartCoroutine(SpawnWave());
        }
    }
    private IEnumerator FadeOut()
    {
        float timeElapse = 0;
        float timeToElapse = 1;
        Color c = waveNumber.color;

        while (timeElapse < timeToElapse)
        {
            timeElapse += Time.deltaTime;
            c.a = Mathf.Lerp(1, 0, timeElapse / timeToElapse);
            waveNumber.color = c;

            yield return null;
        }
        c.a = 0f;
        waveNumber.color = c;
        waveNumber.gameObject.SetActive(false);
    }
}
public class EnemyData
{
    public Vector3 spawnPos;
    public GameObject enemyPrefab;
}