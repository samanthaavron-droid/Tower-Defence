using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevel : MonoBehaviour
{
    [SerializeField] private GameObject housePrefab;
    [SerializeField] private float gameSpeed; //temp
    [SerializeField] private float difficultyLevel; //temp
    [SerializeField] private bool testing;
    void Awake()
    {
        if (testing)
        {
            GlobalSettings.modifier = gameSpeed;
            GlobalSettings.difficulty = difficultyLevel;
        }
    }
    void Start()
    {
        AdjustDifficulty();
    }
    private void AdjustDifficulty()
    {
        List<Vector3Int> cells = new();

        BoundsInt area = new BoundsInt(
            -7,
            -4,
            0,
            8,
            5,
            1
        );

        for (int x = area.xMin; x < area.xMax; x++)
        {
            for (int y = area.yMin; y < area.yMax; y++)
            {
                Vector3Int currentCell = new Vector3Int(x, y, area.zMin);
                cells.Add(currentCell);
            }
        }
        for (int i = 1; i < GlobalSettings.difficulty; i++)
        {
            Vector3Int newPos = cells[Random.Range(0, cells.Count)];
            cells.Remove(newPos);

            Instantiate(housePrefab, newPos, Quaternion.identity);
        }   
    }
}
