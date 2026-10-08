using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Unit : MonoBehaviour
{
    public StatsTemplate statsTemplate;
    [HideInInspector] public StatsRuntime statsRuntime;
    public WeaponLogic weaponLogic;
    public WeaponType weaponType;
    public EnemyType enemyType;
    public GameObject debreePrefab;
    public GameObject projectilePrefab;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    public Image healthBar;
    [HideInInspector] public float maxHealth;
    public bool alreadyPlaced;
    [HideInInspector] public Vector3 direction;
    public TextMeshProUGUI upgradeText;
    public ParticleSystem upgradeEffect;
    public LineRenderer line;
    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        
    }
    public void WeaponCoolDown()
    {
        if (weaponLogic.statsRuntime.cooldown > 0)
        {
            weaponLogic.statsRuntime.cooldown -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        weaponLogic.Attack(this);
    }
    public virtual void TakeDamage(float damage)
    {
        if (statsRuntime == null)
        {
            statsRuntime = new(statsTemplate);
            maxHealth = statsRuntime.health;
        }

        statsRuntime.health -= damage;

        if (statsRuntime.health <= 0)
        {
            Death();
        }

        if (healthBar != null)
        {
            healthBar.fillAmount = statsRuntime.health / maxHealth;
        }
    }
    public virtual void Death()
    {

    }
}
public enum WeaponType
{
    None,
    Turret,
    Rocket,
    Minigun,
    Cannon,
    Laser,
}
public enum EnemyType
{
    None,
    Figher,
    Bomber,
    Helicopter,
    Rocket,
}