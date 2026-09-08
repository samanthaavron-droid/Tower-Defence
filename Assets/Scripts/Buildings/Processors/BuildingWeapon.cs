using UnityEngine;

public class BuildingWeapon //this is the class on which weapons are built
{
    public BuildingStatsTemplate buildingStatsTemplate;
    public BuildingStatsRuntime buildingStats;
    public virtual void Use(BuildingBase user){ }
    public virtual void LevelUp(){ }
}
