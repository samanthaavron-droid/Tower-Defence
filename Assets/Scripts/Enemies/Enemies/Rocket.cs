using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Rocket : WeaponLogic
{
    public Rocket(StatsTemplate stats)
    {
        base.statsRuntime= new(stats);
    }
    public override void Attack(Unit user)
    {
        if (statsRuntime.cooldown > 0 || user == null) return;

        Collider2D hit = OverlapCircleAllCheck(user, statsRuntime, LayerMask.GetMask("Building"));

        if (hit != null)
        {
            GameObject projectile = GameObject.Instantiate(user.GetProjectilePrefab, user.transform.position, Quaternion.identity);

            ProjectileData(projectile, statsRuntime, (hit.transform.position - user.transform.position).normalized, TagHandle.GetExistingTag("Building"), user.GetHitEffect, user, hit.gameObject);

            statsRuntime.cooldown = statsRuntime.rechargeTime;
        }
    }
}
