using UnityEngine;

public class Turret : WeaponLogic
{
    public Turret(StatsTemplate stats)
    {
        base.statsRuntime = new(stats);
    }
    public override void Attack(Unit user)
    {
        if (statsRuntime.cooldown > 0 || user == null) return;

        Collider2D hit = OverlapCircleCheck(user, statsRuntime, LayerMask.GetMask("Enemy"));
        if (hit != null)
        {
            if (OnScreen(hit.transform.position) == false) return;

            FireEffectEmit(user, 30);

            GameObject projectile = GameObject.Instantiate(user.GetProjectilePrefab, user.transform.position, Quaternion.identity);

            Unit enemy = hit.GetComponent<Unit>();
            Projectile enemyPj = hit.GetComponent<Projectile>();

            if (enemy != null)
            {
                ProjectileData(projectile, statsRuntime, (Interception.GetInterceptionPoint(user.transform.position, hit.transform.position, enemy.GetStatsRuntime.speed * enemy.GetDirection, statsRuntime.projectileSpeed) - user.transform.position).normalized, TagHandle.GetExistingTag("Enemy"), user.GetHitEffect, null, null);
            } else if (enemyPj != null)
            {
                ProjectileData(projectile, statsRuntime, (Interception.GetInterceptionPoint(user.transform.position, hit.transform.position, enemyPj.SetStatsRuntime.projectileSpeed * enemyPj.GetSetFuturePos, statsRuntime.projectileSpeed) - user.transform.position).normalized, TagHandle.GetExistingTag("Enemy"), user.GetHitEffect, null, null);
            }


            statsRuntime.cooldown = statsRuntime.rechargeTime;
        }
    }
}
