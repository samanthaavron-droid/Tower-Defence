using UnityEngine;
using System.Collections.Generic;


public class Bomber : EnemyLogic
{
    private List<GameObject> alreadyBombed = new();
    public Bomber(EnemyStatsTemplate stats)
    {
        base.enemyStats= new(stats);
    }
    public override void Attack(EnemyBase user)
    {
        if (enemyStats.cooldown > 0 || user == null) return;

        Collider2D hit = Physics2D.OverlapPoint(user.transform.position, LayerMask.GetMask("Building"));
        if (hit != null)
        {
            if (alreadyBombed.Contains(hit.gameObject) == true) return; //making sure the building isn't bombed twice

            GameObject projectile = GameObject.Instantiate(user.projectilePrefab, user.transform.position, Quaternion.identity);
            BasicEnemyProjectile projectileInfo = projectile.GetComponent<BasicEnemyProjectile>();

            projectileInfo.enemyStats = enemyStats;
            projectileInfo.futurePos = user.transform.position;

            alreadyBombed.Add(hit.gameObject);
            //explosion effect
            enemyStats.cooldown = enemyStats.rechargeTime;
        }
    }
}
