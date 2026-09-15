using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Rocket : EnemyLogic
{
    public Rocket(EnemyStatsTemplate stats)
    {
        base.enemyStats= new(stats);
    }
    public override void Attack(EnemyBase user)
    {
        if (enemyStats.cooldown > 0 || user == null) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(user.transform.position, enemyStats.attackRange, LayerMask.GetMask("Building"));

        if (hits.Length > 0)
        {
            Collider2D hit = FindTarget(hits);

            if (hit != null)
            {
                TargetedEnemyProjectile projectileInfo = user.GetComponent<TargetedEnemyProjectile>();

                projectileInfo.enemyStats = enemyStats;
                projectileInfo.futurePos = (hit.transform.position - user.transform.position).normalized;
                projectileInfo.target = hit.gameObject;
                //user.transform.rotation = (hit.transform.position - user.transform.position);

                enemyStats.cooldown = enemyStats.rechargeTime;
            }
        }
    }
    private Collider2D FindTarget(Collider2D[] hits)
    {
        int randomTarget = Random.Range(0, hits.Length);
        return hits[randomTarget];
    }
}
