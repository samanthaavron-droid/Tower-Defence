using UnityEngine;

public class Turret : WeaponLogic
{
    public Turret(BuildingStatsTemplate stats)
    {
        base.buildingStats = new(stats);
    }
    public override void Attack(BuildingBase user)
    {
        if (buildingStats.cooldown > 0 || user == null) return;

        Collider2D hit = OverlapCircleCheck(user, buildingStats, LayerMask.GetMask("Enemy"));
        if (hit != null)
        {
            GameObject projectile = GameObject.Instantiate(user.projectilePrefab, user.transform.position, Quaternion.identity);

            ProjectileData(projectile, buildingStats, (Interception.GetInterceptionPoint(user.transform.position, hit.transform.position, hit.GetComponent<Rigidbody2D>().linearVelocity, buildingStats.projectileSpeed) - user.transform.position).normalized);

            buildingStats.cooldown = buildingStats.rechargeTime;
        }
    }
    //public override void LevelUp()
    //{

    //}
}
