using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Unit : MonoBehaviour
{
    [SerializeField] protected StatsTemplate statsTemplate;
    public StatsTemplate GetStatsTemplate { get { return statsTemplate; } }
    [HideInInspector] protected StatsRuntime statsRuntime;
    public StatsRuntime GetStatsRuntime { get { return statsRuntime; } set { statsRuntime = value; } }
    protected WeaponLogic weaponLogic;
    public WeaponLogic GetWeaponLogic { get { return weaponLogic; } }
    [SerializeField] protected WeaponType weaponType;
    public WeaponType GetWeaponType {  get { return weaponType; } }
    [SerializeField] protected EnemyType enemyType;
    [SerializeField] protected GameObject debreePrefab;
    [SerializeField] protected GameObject projectilePrefab;
    public GameObject GetProjectilePrefab {  get { return projectilePrefab; } }
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [SerializeField] protected Image healthBar;
    public Image GetHealthBar {  get { return healthBar; } }
    [HideInInspector] protected float maxHealth;
    public float GetMaxHealth { get { return maxHealth; } }
    [SerializeField] protected bool alreadyPlaced;
    public bool SetAlreadyPlaced {set { alreadyPlaced = value; } }
    [HideInInspector] protected Vector3 direction;
    public Vector3 GetDirection { get { return direction; } }
    [SerializeField] protected TextMeshProUGUI upgradeText;
    public TextMeshProUGUI GetUpgradeText { get { return upgradeText; } }
    [SerializeField] protected ParticleSystem upgradeEffect;
    [SerializeField] protected LineRenderer line;
    public LineRenderer GetLine {  get { return line; } }
    [SerializeField] protected ParticleSystem fireEffect;
    public ParticleSystem GetFireEffect { get  { return fireEffect; } }
    [SerializeField] protected ParticleSystem hitEffect;
    public ParticleSystem GetHitEffect { get { return hitEffect; } }
    [SerializeField] protected GameObject attackRadius;
    public GameObject GetAttackRadius { get { return attackRadius; } set { attackRadius = value; } }

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        
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
    Shield,
}
public enum EnemyType
{
    None,
    Figher,
    Bomber,
    Helicopter,
    Rocket,
    Jet,
}