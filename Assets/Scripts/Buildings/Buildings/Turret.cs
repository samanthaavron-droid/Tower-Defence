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

        Collider2D hit = Physics2D.OverlapCircle(user.transform.position, buildingStats.attackRange, LayerMask.GetMask("Enemy"));
        if (hit != null)
        {
            GameObject projectile = GameObject.Instantiate(user.projectilePrefab, user.transform.position, Quaternion.identity);
            BasicTowerProjectile projectileInfo = projectile.GetComponent<BasicTowerProjectile>();
            projectileInfo.buildingStats = buildingStats;

            projectileInfo.futurePos = (Interception.GetInterceptionPoint(user.transform.position, hit.transform.position, hit.GetComponent<Rigidbody2D>().linearVelocity, buildingStats.projectileSpeed) - user.transform.position).normalized;

            buildingStats.cooldown = buildingStats.rechargeTime;
        }
    }
    public override void LevelUp()
    {

    }
}
