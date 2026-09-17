using UnityEngine;

public class Minigun : BuildingWeapon
{
    public Minigun(BuildingStatsTemplate stats)
    {
        base.buildingStats = new(stats);
    }
    public override void Use(BuildingBase user)
    {
        if (buildingStats.cooldown > 0 || user == null) return;

        Collider2D hit = Physics2D.OverlapCircle(user.transform.position, buildingStats.attackRange, LayerMask.GetMask("Enemy"));
        if (hit != null)
        {
            GameObject projectile = GameObject.Instantiate(user.projectilePrefab, user.transform.position, Quaternion.identity);
            BasicTowerProjectile projectileInfo = projectile.GetComponent<BasicTowerProjectile>();

            projectileInfo.buildingStats = buildingStats;
            projectileInfo.futurePos = (hit.transform.position - user.transform.position).normalized;
            projectile.transform.up = (hit.transform.position - user.transform.position).normalized;

            buildingStats.cooldown = buildingStats.rechargeTime;
        }
    }
    public override void LevelUp()
    {

    }
}
