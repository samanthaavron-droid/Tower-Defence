using UnityEngine;

public class Minigun : WeaponLogic
{
    private Animator animator;
    public Minigun(StatsTemplate stats, Unit user)
    {
        base.statsRuntime = new(stats);
        animator = user.GetComponent<Animator>();
    }
    public override void Attack(Unit user)
    {
        if (statsRuntime.cooldown > 0) return;

        Collider2D hit = OverlapCircleCheck(user, statsRuntime, LayerMask.GetMask("Enemy"));
        if (hit != null)
        {
            if (OnScreen(hit.transform.position) == false) return;

            FireEffectEmit(user, 30);
            animator.SetTrigger("Shoot");
            
            GameObject projectile = GameObject.Instantiate(user.GetProjectilePrefab, user.transform.position, Quaternion.identity);

            ProjectileData(projectile, statsRuntime, (hit.transform.position - user.transform.position).normalized, TagHandle.GetExistingTag("Enemy"), user.GetHitEffect, null, null);

            statsRuntime.cooldown = statsRuntime.rechargeTime;
        }
    }
}
