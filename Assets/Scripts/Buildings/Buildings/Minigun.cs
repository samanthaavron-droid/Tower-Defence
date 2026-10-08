using UnityEngine;

public class Minigun : WeaponLogic
{
    public Minigun(StatsTemplate stats)
    {
        base.statsRuntime = new(stats);
    }
    public override void Attack(Unit user)
    {
        if (statsRuntime.cooldown > 0) return;

        Collider2D hit = OverlapCircleCheck(user, statsRuntime, LayerMask.GetMask("Enemy"));
        if (hit != null)
        {
            if (OnScreen(hit.transform.position) == false) return;
            
            GameObject projectile = GameObject.Instantiate(user.projectilePrefab, user.transform.position, Quaternion.identity);

            ProjectileData(projectile, statsRuntime, (hit.transform.position - user.transform.position).normalized, TagHandle.GetExistingTag("Enemy"), user);

            statsRuntime.cooldown = statsRuntime.rechargeTime;
        }
    }
}
