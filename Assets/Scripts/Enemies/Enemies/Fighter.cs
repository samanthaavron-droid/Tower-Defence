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

        enemyStats.cooldown = enemyStats.rechargeTime;
    }
}
