using UnityEngine;

public class EnemyStatsRuntinme
{
    //this is for runtime reference
    public float health { get; set; }
    public float attackRange { get; set; }
    public float projectileSpeed { get; set; }
    public float damage { get; set; }
    public float rechargeTime { get; set; }
    public float speed { get; set; }
    public float cooldown { get; set; }
    public float projectileHealth { get; set; }
    public EnemyStatsRuntinme(EnemyStatsTemplate s)
    {
        //this is for when the enemy is created
        health = s.health;
        attackRange = s.attackRange;
        projectileSpeed = s.projectileSpeed;
        damage = s.damage;
        rechargeTime = s.rechargeTime;
        speed = s.speed;
        projectileHealth = s.projectileHealth;
    }
}
