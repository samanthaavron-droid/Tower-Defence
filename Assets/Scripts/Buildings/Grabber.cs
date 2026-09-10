using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject prefabIU;
    public BuildingStatsTemplate buildingStatsTemplate;
    [HideInInspector] public float buildingCost;

    private void Awake()
    {
        if (buildingStatsTemplate != null)
        {
            buildingCost = buildingStatsTemplate.buildingCost;
        } else
        {
            buildingCost = 0;
        }
    }
}
