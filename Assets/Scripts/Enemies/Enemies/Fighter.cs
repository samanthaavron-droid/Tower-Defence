using UnityEngine;

public class Fighter : WeaponLogic
{
    public Fighter(EnemyStatsTemplate stats)
    {
        base.enemyStats= new(stats);
    }
    public override void Attack(EnemyBase user)
    {
        if (enemyStats.cooldown > 0 || user == null) return;

        RaycastHit2D hit = RaycastCheck(user, Vector2.left, enemyStats, LayerMask.GetMask("Building"));
        if (hit.collider == true)
        {
            GameObject projectile = GameObject.Instantiate(user.projectilePrefab, user.transform.position, Quaternion.identity);

            ProjectileData(projectile, enemyStats, (hit.transform.position - user.transform.position).normalized);

            enemyStats.cooldown = enemyStats.rechargeTime;
        }
    }
}
