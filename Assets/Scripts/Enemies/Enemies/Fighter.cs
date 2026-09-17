using UnityEngine;

public class Fighter : EnemyLogic
{
    public Fighter(EnemyStatsTemplate stats)
    {
        base.enemyStats= new(stats);
    }
    public override void Attack(EnemyBase user)
    {
        if (enemyStats.cooldown > 0 || user == null) return;

        RaycastHit2D hit = Physics2D.Raycast(user.transform.position, Vector2.left, enemyStats.attackRange, LayerMask.GetMask("Building"));
        if (hit.collider == true)
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
