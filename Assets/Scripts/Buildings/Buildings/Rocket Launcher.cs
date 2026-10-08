using UnityEngine;

public class RocketLauncher : WeaponLogic
{
    public RocketLauncher(StatsTemplate stats)
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

            ProjectileData(projectile, statsRuntime, (Interception.GetInterceptionPoint(user.transform.position, hit.transform.position, hit.GetComponent<Rigidbody2D>().linearVelocity, statsRuntime.projectileSpeed) - user.transform.position).normalized, TagHandle.GetExistingTag("Enemy"), user, hit.gameObject);

            statsRuntime.cooldown = statsRuntime.rechargeTime;
        }
    }
}
