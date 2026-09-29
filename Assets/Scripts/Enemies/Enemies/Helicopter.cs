using UnityEngine;

public class Helicopter : WeaponLogic
{
    public Helicopter(EnemyStatsTemplate stats)
    {
        base.enemyStats= new(stats);
    }
    public override void Attack(EnemyBase user)
    {
        if (enemyStats.cooldown > 0 || user == null) return;

        Collider2D hit = OverlapCircleCheck(user, enemyStats, LayerMask.GetMask("Building"));
        if (hit != null)
        {
            GameObject projectile = GameObject.Instantiate(user.projectilePrefab, user.transform.position, Quaternion.identity);

            ProjectileData(projectile, enemyStats, (hit.transform.position - user.transform.position).normalized);

            enemyStats.cooldown = enemyStats.rechargeTime;
        }
    }
}
