using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] private EnemyWave[] enemyWaves;
    private int enemyWaveIndex = 0;
    private List<EnemyBase> activeEnemies = new();
    [SerializeField] private float spawnDelay;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    void Update()
    {
        
    }
    private IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(spawnDelay);

        activeEnemies.AddRange(enemyWaves[enemyWaveIndex].enemyPrefab);
    
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            Instantiate(enemyWaves[enemyWaveIndex].enemyPrefab[i], enemyWaves[enemyWaveIndex].spawnPos[i].position, Quaternion.identity);
        }
        enemyWaveIndex++;
    }
    public void EnemyDied(EnemyBase enemy)
    {
        activeEnemies.Remove(enemy);
        Destroy(enemy.gameObject);
        if (activeEnemies.Count <= 0)
        {
            if (enemyWaveIndex < enemyWaves.Length)
            {
                StartCoroutine(SpawnWave());
            }
        }
    }
}
[System.Serializable]
public struct EnemyWave
{
    public EnemyBase[] enemyPrefab;
    public Transform[] spawnPos;
}