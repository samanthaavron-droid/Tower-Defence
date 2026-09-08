using UnityEngine;

public class BuildingStatsRuntime
{
    //this is for runtime reference
    public float health { get; set; }
    public float attackRange { get; set; }
    public float projectileSpeed { get; set; }
    public float damage { get; set; }
    public float rechargeTime { get; set; }
    public float debreeSize { get; set; }
    public float buildingSpeed { get; set; }
    public float buildingCost { get; set; }
    public bool priority { get; set; }
    public float cooldown { get; set; }
    public float currentLeveled { get; set; } = 1;

    public BuildingStatsRuntime(BuildingStatsTemplate s)
    {
        //this is for when the building is created
        health = s.health;
        attackRange = s.attackRange;
        projectileSpeed = s.projectileSpeed;
        damage = s.damage;
        rechargeTime = s.rechargeTime;
        debreeSize = s.debreeSize;
        buildingSpeed = s.buildingSpeed;
        buildingCost = s.buildingCost;
        priority = s.priority;
    }
}