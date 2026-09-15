using UnityEngine;

public class Cannon : BuildingWeapon
{
    public Cannon(BuildingStatsTemplate stats)
    {
        base.buildingStats = new(stats);
    }
    public override void Use(BuildingBase user)
    {
        if (buildingStats.cooldown > 0 || user == null) return;

        RaycastHit2D hit = Physics2D.Raycast(user.transform.position, Vector2.right, buildingStats.attackRange, LayerMask.GetMask("Enemy"));
        if (hit == true)
        {
            GameObject projectile = GameObject.Instantiate(user.projectilePrefab, user.transform.position, Quaternion.identity);
            BasicTowerProjectile projectileInfo = projectile.GetComponent<BasicTowerProjectile>();

            projectileInfo.buildingStats = buildingStats;
            projectileInfo.futurePos = Vector2.right;

            buildingStats.cooldown = buildingStats.rechargeTime;
        }
    }
    public override void LevelUp()
    {

    }
}
