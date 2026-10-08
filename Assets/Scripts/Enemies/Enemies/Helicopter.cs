using UnityEngine;

public class Helicopter : WeaponLogic
{
    public Helicopter(StatsTemplate stats)
    {
        base.statsRuntime = new(stats);
    }
    public override void Attack(Unit user)
    {
        if (statsRuntime.cooldown > 0 || user == null) return;

        Collider2D hit = OverlapCircleCheck(user, statsRuntime, LayerMask.GetMask("Building"));
        if (hit != null)
        {
            GameObject projectile = GameObject.Instantiate(user.projectilePrefab, user.transform.position, Quaternion.identity);

            ProjectileData(projectile, statsRuntime, (hit.transform.position - user.transform.position).normalized, TagHandle.GetExistingTag("Building"), user);

            statsRuntime.cooldown = statsRuntime.rechargeTime;
        }
    }
}
