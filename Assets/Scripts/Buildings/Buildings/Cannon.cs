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
        if (hit.collider == true)
        {
            if (OnScreen(hit.transform.position) == false) return;

            GameObject projectile = GameObject.Instantiate(user.projectilePrefab, user.transform.position, Quaternion.identity);

            ProjectileData(projectile, statsRuntime, Vector2.right, TagHandle.GetExistingTag("Enemy"), user);

            statsRuntime.cooldown = statsRuntime.rechargeTime;
        }
    }
}
