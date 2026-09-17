using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyWave[] enemyWaves;
    private int enemyWaveIndex = 0;
    private List<GameObject> activeEnemies = new();
    [SerializeField] private float spawnDelay;
    void Start()
    {
        StartCoroutine(SpawnWave());
        EnemyBase.enemyDeath += EnemyDied;
    }

    void Update()
    {
        
    }
    private IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(spawnDelay);
        Debug.Log("New Wave! #" + enemyWaveIndex);
    
        for (int i = 0; i < enemyWaves[enemyWaveIndex].enemyPrefab.Length; i++)
        {
            GameObject enemy = Instantiate(enemyWaves[enemyWaveIndex].enemyPrefab[i], enemyWaves[enemyWaveIndex].spawnPos[i].position, Quaternion.identity);
            activeEnemies.Add(enemy);
        }
        enemyWaveIndex++;
    }
    public void EnemyDied(EnemyBase enemy)
    {
        activeEnemies.Remove(enemy.gameObject);

        Builder.instance.AddResource(enemy.enemyStats.worth); //adding money to the builder
        Destroy(enemy.gameObject);

        Debug.Log(activeEnemies.Count);

        if (activeEnemies.Count == 0)
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
    public GameObject[] enemyPrefab;
    public Transform[] spawnPos;
}