using System.Collections;
using UnityEngine;

public class Debree : MonoBehaviour
{
    public BuildingStatsRuntime buildingStats;

    public void StartRemoval()
    {
        //add interface of the timer
        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(buildingStats.debreeSize);
        Destroy(gameObject);
    }
}
