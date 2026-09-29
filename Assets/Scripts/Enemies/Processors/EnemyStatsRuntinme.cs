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
    public float worth {  get; set; }
    public EnemyStatsRuntinme(EnemyStatsTemplate s)
    {
        //this is for when the enemy is created
        health = s.health;
        attackRange = s.attackRange;
        projectileSpeed = s.projectileSpeed * GlobalSettings.modifier;
        damage = s.damage;
        rechargeTime = s.rechargeTime / GlobalSettings.modifier;
        speed = s.speed * GlobalSettings.modifier;
        projectileHealth = s.projectileHealth;
        worth = s.worth;
    }
}
