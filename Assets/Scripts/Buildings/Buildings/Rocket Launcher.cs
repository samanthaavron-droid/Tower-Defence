using UnityEngine;

public class RocketLauncher : BuildingWeapon
{
    public RocketLauncher(BuildingStatsTemplate stats)
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
            TargetedTowerProjectile projectileInfo = projectile.GetComponent<TargetedTowerProjectile>();

            projectileInfo.buildingStats = buildingStats;
            projectileInfo.target = hit.gameObject;
            projectileInfo.futurePos = (Interception.GetInterceptionPoint(user.transform.position, hit.transform.position, hit.GetComponent<Rigidbody2D>().linearVelocity, buildingStats.projectileSpeed) - user.transform.position).normalized;
            projectile.transform.up = (projectileInfo.futurePos - user.transform.position).normalized;

            buildingStats.cooldown = buildingStats.rechargeTime;
        }
    }
    public override void LevelUp()
    {

    }
}
