using System.Collections;
using UnityEngine;

public class BuildingBase : MonoBehaviour //this code runs when the building is created
{
    BuildingBase user;
    public BuildingStatsTemplate buildingStatsTemplate;
    public BuildingStatsRuntime buildingStats;
    public BuildingWeapon weapon = null;
    public WeaponType weaponType;
    public GameObject debreePrefab;
    public GameObject projectilePrefab;
    void Start()
    {
        buildingStats = new(buildingStatsTemplate);
        user = this;
        StartCoroutine(StartBuilding());
    }
    void Update()
    {
        if (weapon != null)
        {
            WeaponCooldownUpdate();
            Attack();
        }
    }
    private IEnumerator StartBuilding()
    {
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
            //gameover
        }
    }
}
public enum WeaponType
{
    None,
    Turret,
    Rocket
}