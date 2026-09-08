using UnityEngine;

[CreateAssetMenu(fileName = "BuildingStatsTemplate", menuName = "Scriptable Objects/Building Stats Template")]

public class BuildingStatsTemplate : ScriptableObject
{
    public float health;
    public float attackRange;
    public float projectileSpeed;
    public float damage;
    public float rechargeTime;
    public float debreeSize;
    public float buildingSpeed;
    public float buildingCost;
    public bool priority;
}
