using System;
using UnityEngine;
public class EnemyBase : Unit //this code runs when the enemy is created
{
    public static event Action<EnemyBase> enemyDeath;
    public override void Start()
    {
        SetType();
        statsRuntime = new(statsTemplate);
        direction = Vector3.left;
    }

    public override void Update()
    {
        if (weaponLogic != null)
        {
            WeaponCoolDown();
            Attack();
        }
        Move();
    }
    private void SetType()
    {
        switch (enemyType)
        {
            case EnemyType.None:
                weaponLogic = null;
                break;
            case EnemyType.Figher:
                weaponLogic = new Fighter(statsTemplate);
                break;
            case EnemyType.Bomber:
                weaponLogic = new Bomber(statsTemplate);
                break;
            case EnemyType.Helicopter:
                weaponLogic = new Helicopter(statsTemplate);
                break;
            case EnemyType.Rocket:
                weaponLogic = new Rocket(statsTemplate);
                break;
        }
    }
    private void Move()
    {
        transform.position += direction * statsRuntime.speed * Time.deltaTime;
    }
    public override void TakeDamage(float damage)
    {
        statsRuntime.health -= damage;
        if (statsRuntime.health <= 0)
        {
            enemyDeath?.Invoke(this);
        }
    }
}