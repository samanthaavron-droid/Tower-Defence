using System.Collections;
using UnityEngine;

public class Debree : MonoBehaviour
{
    public BuildingStatsRuntime buildingStats;

    public void StartRemoval(Builder builder)
    {
        //add interface of the timer
        StartCoroutine(Timer(builder));
    }
    private IEnumerator Timer(Builder builder)
    {
        yield return new WaitForSeconds(buildingStats.debreeSize);
        builder.occupiedCells.Remove(builder.tilemap.WorldToCell(transform.position));
        Destroy(gameObject);
    }
}
