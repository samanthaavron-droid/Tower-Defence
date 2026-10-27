using UnityEngine;

public class Fighter : WeaponLogic
{
    public Fighter(StatsTemplate stats)
    {
        base.statsRuntime= new(stats);
    }
    public override void Attack(Unit user)
    {
        if (statsRuntime.cooldown > 0 || user == null) return;

        RaycastHit2D hit = RaycastCheck(user, Vector2.left, statsRuntime, LayerMask.GetMask("Building"));
        if (hit.collider == true)
        {
            GameObject projectile = GameObject.Instantiate(user.GetProjectilePrefab, user.transform.position, Quaternion.identity);

            ProjectileData(projectile, statsRuntime, (new Vector3(hit.point.x, hit.point.y, 0) - user.transform.position).normalized, TagHandle.GetExistingTag("Building"), user.GetHitEffect, null, null);

            statsRuntime.cooldown = statsRuntime.rechargeTime;
        }
    }
}
