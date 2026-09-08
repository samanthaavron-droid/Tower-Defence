using UnityEngine;

public class Turret : BuildingWeapon
{
    public Turret(BuildingStatsTemplate stats)
    {
        base.buildingStats = new(stats);
    }
    public override void Use(BuildingBase user)
    {
        if (buildingStats.cooldown > 0 || user == null) return;

        buildingStats.cooldown = buildingStats.rechargeTime;
    }
    public override void LevelUp()
    {

    }
}
