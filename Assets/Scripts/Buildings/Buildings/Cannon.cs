using UnityEngine;

public class Cannon : WeaponLogic
{
    public Cannon(BuildingStatsTemplate stats)
    {
        base.buildingStats = new(stats);
    }
    public override void Attack(BuildingBase user)
    {
        if (buildingStats.cooldown > 0 || user == null) return;

        RaycastHit2D hit = RaycastCheck(user, Vector2.right, buildingStats, LayerMask.GetMask("Enemy"));
        if (hit == true)
        {
            GameObject projectile = GameObject.Instantiate(user.projectilePrefab, user.transform.position, Quaternion.identity);

            ProjectileData(projectile, buildingStats, Vector2.right);

            buildingStats.cooldown = buildingStats.rechargeTime;
        }
    }
    //public override void LevelUp()
    //{

    //}
}
