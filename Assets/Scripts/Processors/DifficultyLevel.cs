using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevel : MonoBehaviour
{
    [SerializeField] private GameObject housePrefab;
    [SerializeField] private GameObject debreePrefab;
    [SerializeField] private float gameSpeed; //temp
    [SerializeField] private float difficultyLevel; //temp
    void Awake()
    {

    }
    void Start()
    {
        AdjustDifficulty();
    }
    private void AdjustDifficulty()
    {
        List<Vector3Int> cells = new();

        BoundsInt areaCiv = new BoundsInt(
            -5,
            -3,
            0,
            6,
            5,
            1
        );
        for (int x = areaCiv.xMin; x < areaCiv.xMax; x++)
        {
            for (int y = areaCiv.yMin; y < areaCiv.yMax; y++)
            {
                cells.Add(new Vector3Int(x, y, 0));
            }
        }
        for (int i = 1; i < GlobalSettings.GetSetDifficulty; i++)
        {
            if (cells.Count == 0) break;

            int randomIndex = Random.Range(0, cells.Count);
            Vector3Int newPos = cells[randomIndex];
            cells.RemoveAt(randomIndex);
            Builder.instance.getOccupiedCells.Add(newPos);

            Instantiate(housePrefab, Builder.instance.getTilemap.GetCellCenterWorld(newPos), Quaternion.identity);
        }

        BoundsInt areaDeb = new BoundsInt(
            -5,
            -3,
            0,
            10,
            5,
            1
        );
        for (int x = areaDeb.xMin; x < areaDeb.xMax; x++)
        {
            for (int y = areaDeb.yMin; y < areaDeb.yMax; y++)
            {
                Vector3Int currentCell = new Vector3Int(x, y, 0);
                if (cells.Contains(currentCell) == false && Builder.instance.getOccupiedCells.Contains(currentCell) == false)
                    cells.Add(currentCell);
            }
        }
        for (int i = 2; i < GlobalSettings.GetSetDifficulty; i++)
        {
            if (cells.Count == 0) break;

            int randomIndex = Random.Range(0, cells.Count);
            Vector3Int newPos = cells[randomIndex];
            cells.RemoveAt(randomIndex);

            Builder.instance.getOccupiedCells.Add(newPos);

            GameObject newDebree = Instantiate(debreePrefab, Builder.instance.getTilemap.GetCellCenterWorld(newPos), Quaternion.identity);
            Debree debreeStats = newDebree.GetComponent<Debree>();

            StatsTemplate newStats = ScriptableObject.CreateInstance<StatsTemplate>();
            newStats.debreeSize = Random.Range(5, 15);
            debreeStats.statsRuntime = new(newStats);
        }
    }
}
