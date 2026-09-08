using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] private EnemyWave[] enemyWaves;
    [SerializeField] private Transform[] spawnPoints;
    private List<EnemyBase> activeEnemies = new();
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void SpawnWave()
    {

    }
}
[System.Serializable]
public struct EnemyWave
{
    public EnemyBase[] enemyPrefab;
}