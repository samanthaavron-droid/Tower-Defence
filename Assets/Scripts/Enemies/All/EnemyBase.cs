using System;
using UnityEngine;
public class EnemyBase : Unit //this code runs when the enemy is created
{
    public static event Action<EnemyBase> enemyDeath;
    protected override void Start()
    {
        SetType();
        statsRuntime = new(statsTemplate);
        direction = Vector3.left;

        maxHealth = statsRuntime.health;

        if (healthBar != null)
            healthBar.fillAmount = 1f;
    }

    protected override void Update()
    {
        if (weaponLogic != null)
        {
            weaponLogic.Update(this);
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
            case EnemyType.Jet:
                weaponLogic = new Jet(statsTemplate);
                break;
        }
    }
    private void Move()
    {
        transform.position += direction * statsRuntime.speed * Time.deltaTime;
    }
    protected override void Death()
    {
        enemyDeath?.Invoke(this);
    }
}