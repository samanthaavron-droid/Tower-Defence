using UnityEngine;

public class EnemyLogic //this is the class on which weapons are built
{
    public EnemyStatsTemplate enemyStatsTemplate;
    public EnemyStatsRuntinme enemyStats;
    public virtual void Attack(EnemyBase user) { }
}
