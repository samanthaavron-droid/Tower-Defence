using UnityEngine;

public class Helicopter : EnemyLogic
{
    public Helicopter(EnemyStatsTemplate stats)
    {
        base.enemyStats= new(stats);
    }
    public override void Attack(EnemyBase user)
    {
        if (enemyStats.cooldown > 0 || user == null) return;

        Collider2D hit = Physics2D.OverlapCircle(user.transform.position, enemyStats.attackRange, LayerMask.GetMask("Building"));
        if (hit != null)
        {
            GameObject projectile = GameObject.Instantiate(user.projectilePrefab, user.transform.position, Quaternion.identity);
            BasicEnemyProjectile projectileInfo = projectile.GetComponent<BasicEnemyProjectile>();

            projectileInfo.enemyStats = enemyStats;
            projectileInfo.futurePos = (hit.transform.position - user.transform.position).normalized;
            projectile.transform.up = (hit.transform.position - user.transform.position).normalized;

            enemyStats.cooldown = enemyStats.rechargeTime;
        }
    }
}
