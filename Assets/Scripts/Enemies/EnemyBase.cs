using UnityEngine;

public class EnemyBase : MonoBehaviour //this code runs when the enemy is created
{
    EnemyBase user;
    public EnemyStatsTemplate enemyStatsTemplate;
    public EnemyStatsRuntinme enemyStats;
    public EnemyLogic enemyLogic;
    public EnemyType enemyType;
    private void OnValidate()
    {
        SetType();
    }
    void Start()
    {
        enemyStats = new(enemyStatsTemplate);
        user = this;
    }

    void Update()
    {
        WeaponCooldownUpdate();
        Move();
    }
    private void WeaponCooldownUpdate()
    {
        if (enemyLogic == null) return;

        if (enemyLogic.enemyStats.cooldown > 0)
        {
            enemyLogic.enemyStats.cooldown -= Time.deltaTime;
        }
    }
    private void SetType()
    {
        switch (enemyType)
        {
            case EnemyType.None:
                enemyLogic = null;
                break;
            case EnemyType.Figher:
                enemyLogic = new Fighter(enemyStatsTemplate);
                break;
            case EnemyType.Bomber:
                //enemyLogic = new Bomber(enemyStatsTemplate);
                break;
        }
    }
    private void Move()
    {
        transform.position += Vector3.left * enemyStats.speed * Time.deltaTime;
    }
    public void Attack()
    {
        if (enemyLogic != null)
        {
            enemyLogic.Attack(user);
        }
    }
    public void TakeDamage(float damage)
    {
        enemyStats.health -= damage;
        if (enemyStats.health <= 0)
        {
            Death();
        }
    }
    private void Death()
    {

    }
}
public enum EnemyType
{
    None,
    Figher,
    Bomber,
    Test
}