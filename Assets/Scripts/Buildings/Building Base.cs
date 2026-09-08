using UnityEngine;

public class BuildingBase : MonoBehaviour //this code runs when the building is created
{
    BuildingBase user;
    public BuildingStatsTemplate buildingStatsTemplate;
    public BuildingStatsRuntime buildingStats;
    public BuildingWeapon weapon;
    public WeaponType weaponType;
    public GameObject debreePrefab;
    private void OnValidate()
    {
        SetWeapon();
    }
    void Start()
    {
        buildingStats = new(buildingStatsTemplate);
        user = this;
    }
    void Update()
    {
        WeaponCooldownUpdate();
    }
    private void WeaponCooldownUpdate()
    {
        if (weapon == null) return;

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
        if (weapon != null)
        {
            weapon.Use(user);
        }
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
        GameObject debree = Instantiate(debreePrefab, transform.position, Quaternion.identity);
        debree.GetComponent<Debree>().buildingStats = buildingStats;
    }
}
public enum WeaponType
{
    None,
    Turret,
    Rocket
}