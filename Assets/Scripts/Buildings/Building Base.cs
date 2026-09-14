using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBase : MonoBehaviour //this code runs when the building is created
{
    BuildingBase user;
    public BuildingStatsTemplate buildingStatsTemplate;
    public BuildingStatsRuntime buildingStats;
    public BuildingWeapon weapon = null;
    public WeaponType weaponType;
    public GameObject debreePrefab;
    public GameObject projectilePrefab;
    private SpriteRenderer _spriteRenderer;
    public Image healthBar;
    private float maxHealth;
    void Start()
    {
        if (buildingStats == null)
        {
            buildingStats = new(buildingStatsTemplate);
            user = this;
            maxHealth = buildingStats.health;
        }
        healthBar.fillAmount = 1f;
    }
    void Update()
    {
        if (weapon != null)
        {
            WeaponCooldownUpdate();
            Attack();
        }
    }
    public IEnumerator StartBuilding()
    {
        buildingStats = new(buildingStatsTemplate);
        user = this;
        maxHealth = buildingStats.health;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        Color color = _spriteRenderer.color;
        color.a = 0f;
        _spriteRenderer.color = color;

        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(buildingStats.buildingSpeed);

        SetWeapon();
    }
    private void WeaponCooldownUpdate()
    {
        if (weapon.buildingStats.cooldown > 0)
        {
            weapon.buildingStats.cooldown -= Time.deltaTime;
        }
    }
    private void SetWeapon()
    {
        switch (weaponType)
        {
            case WeaponType.None:
                weapon = null;
                break;
            case WeaponType.Turret:
                weapon = new Turret(buildingStatsTemplate);
                break;
            case WeaponType.Rocket:
                //weapon = new Rocket(buildingStatsTemplate);
                break;
            case WeaponType.Minigun:
                weapon = new Minigun(buildingStatsTemplate);
                break;
        }
    }
    public void Attack()
    {
        weapon.Use(user);
    }
    public void TakeDamage(float damage)
    {
        buildingStats.health -= damage;
        if (buildingStats.health <= 0)
        {
            Death();
        }
        Debug.Log(gameObject + " has " + buildingStats.health + " health left");

        healthBar.fillAmount = buildingStats.health / maxHealth;
    }
    private void Death()
    {
        if (buildingStats.priority == false)
        {
            GameObject debree = Instantiate(debreePrefab, transform.position, Quaternion.identity);
            debree.GetComponent<Debree>().buildingStats = buildingStats;
            Destroy(gameObject);
        } else
        {
#if UNITY_EDITOR
            // stops Play Mode inside the Unity Editor
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
    private IEnumerator FadeIn()
    {
        float timeElapse = 0;
        float timeToElapse = buildingStats.buildingSpeed;
        Color c = _spriteRenderer.color;

        while (timeElapse < timeToElapse)
        {
            timeElapse += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, timeElapse / timeToElapse);
            _spriteRenderer.color = c;

            yield return null;
        }
        c.a = 1f;
        _spriteRenderer.color = c;
    }
}
public enum WeaponType
{
    None,
    Turret,
    Rocket,
    Minigun,
}