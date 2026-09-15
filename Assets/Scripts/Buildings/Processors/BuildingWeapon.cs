using UnityEngine;

public abstract class BuildingWeapon //this is the class on which weapons are built
{
    public BuildingStatsTemplate buildingStatsTemplate;
    public BuildingStatsRuntime buildingStats;
    public abstract void Use(BuildingBase user);
    public abstract void LevelUp();
}
