using UnityEngine;

public class Cannon : WeaponLogic
{
    public Cannon(StatsTemplate stats)
    {
        base.statsRuntime = new(stats);
    }
    public override void Attack(Unit user)
    {
        if (statsRuntime.cooldown > 0) return;

        RaycastHit2D hit = RaycastCheck(user, Vector2.right, statsRuntime, LayerMask.GetMask("Enemy"));
        RaycastHit2D hitBack = RaycastCheck(user, Vector2.left, statsRuntime, LayerMask.GetMask("Enemy"));
        if (hit.collider == true)
        {
            if (OnScreen(hit.transform.position) == false) return;

            FireEffectPlay(user);

            GameObject projectile = GameObject.Instantiate(user.GetProjectilePrefab, user.transform.position, Quaternion.identity);

            ProjectileData(projectile, statsRuntime, Vector2.right, TagHandle.GetExistingTag("Enemy"), user.GetHitEffect, null, null);

            statsRuntime.cooldown = statsRuntime.rechargeTime;
        } else if (hitBack.collider == true)
        {
            if (OnScreen(hitBack.transform.position) == false) return;

            FireEffectPlay(user);

            GameObject projectile = GameObject.Instantiate(user.GetProjectilePrefab, user.transform.position, Quaternion.identity);

            ProjectileData(projectile, statsRuntime, Vector2.right, TagHandle.GetExistingTag("Enemy"), user.GetHitEffect, null, null);

            statsRuntime.cooldown = statsRuntime.rechargeTime;
        }
    }
}
