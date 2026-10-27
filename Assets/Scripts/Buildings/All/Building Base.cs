using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuildingBase : Unit //this code runs when the building is created
{
    [SerializeField] private bool x, y;
    void Awake()
    {
        if (upgradeText != null)
            upgradeText.gameObject.SetActive(false);
        if (attackRadius != null)
            DrawRadius();
    }
    public override void Start()
    {
        if (healthBar != null)
            healthBar.fillAmount = 1f;
    }
    public override void Update()
    {
        if (weaponLogic != null)
        {
            WeaponCoolDown();
            Attack();
        }
    }
    public void LevelUp()
    {
        if (weaponLogic != null)
            if (statsRuntime.currentLevel < 3)
            {
                weaponLogic.LevelUpTower(this);
                upgradeEffect.Play();

                if (statsRuntime.currentLevel == 3)
                {
                    upgradeText.gameObject.SetActive(false);
                    upgradeText = null;
                }
            }
    }
    private void DrawRadius()
    {
        float radius = statsTemplate.attackRange / transform.localScale.x * 2;
        float radiusx, radiusy;

        if (x == true)
            radiusx = radius;
        else
            radiusx = transform.localScale.x;

        if (y == true)
            radiusy = radius;
        else
            radiusy = transform.localScale.y;

        attackRadius.transform.localScale = new Vector3(radiusx, radiusy, 1);
        attackRadius.SetActive(true);
    }
    public IEnumerator StartBuilding()
    {
        if (statsRuntime == null)
        {
            statsRuntime = new(statsTemplate);
            maxHealth = statsRuntime.health;
        }

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(statsRuntime.buildingSpeed);
        if (attackRadius != null) attackRadius.SetActive(false);

        SetWeapon();
    }
    private void SetWeapon()
    {
        switch (weaponType)
        {
            case WeaponType.None:
                weaponLogic = null;
                break;
            case WeaponType.Turret:
                weaponLogic = new Turret(statsTemplate);
                break;
            case WeaponType.Rocket:
                weaponLogic = new RocketLauncher(statsTemplate);
                break;
            case WeaponType.Minigun:
                weaponLogic = new Minigun(statsTemplate, this);
                break;
            case WeaponType.Cannon:
                weaponLogic = new Cannon(statsTemplate);
                break;
            case WeaponType.Laser:
                weaponLogic = new Laser(statsTemplate);
                break;
            case WeaponType.Shield:
                weaponLogic = new ShieldGenerator(statsTemplate, this);
                break;
        }

        if (weaponLogic != null)
        {
            weaponLogic.SetGetStatsRuntime = this.statsRuntime;
        }
    }
    public override void Death()
    {
        if (statsRuntime.priority == false)
        {
            GameObject debree = Instantiate(debreePrefab, transform.position, Quaternion.identity);
            Debree debS = debree.GetComponent<Debree>();

            debS.statsRuntime = statsRuntime;

            Destroy(gameObject);
        } else
        {
            PauseMenu.instance.EndGame();
        }
    }
    private IEnumerator FadeIn()
    {
        float timeElapse = 0;
        float timeToElapse = statsRuntime.buildingSpeed;
        Color c = spriteRenderer.color;

        while (timeElapse < timeToElapse)
        {
            timeElapse += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, timeElapse / timeToElapse);
            spriteRenderer.color = c;

            yield return null;
        }
        c.a = 1f;
        spriteRenderer.color = c;
    }
}