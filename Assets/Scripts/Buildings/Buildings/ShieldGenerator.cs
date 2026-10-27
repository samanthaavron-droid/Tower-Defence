using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : WeaponLogic
{
    private bool shieldActive;
    public ShieldGenerator(StatsTemplate stats, Unit user)
    {
        base.statsRuntime = new(stats);

        if (shieldActive == false)
        {
            shieldActive = true;
            user.GetAttackRadius.SetActive(true);
        }
    }
    public override void Attack(Unit user)
    {
        if (statsRuntime.cooldown > 0 || user == null) return;

        if (statsRuntime.health < user.GetMaxHealth)
        {
            FireEffectPlay(user);

            statsRuntime.health += 1f;

            if (user.GetHealthBar != null)
            {
                user.GetHealthBar.fillAmount = statsRuntime.health / user.GetMaxHealth;
            }

            statsRuntime.cooldown = statsRuntime.rechargeTime;
        }
        else if (statsRuntime.health == user.GetMaxHealth)
        {
            user.GetFireEffect.Stop();
        }
    }
    public override void LevelUpTower(Unit user)
    {
        base.LevelUpTower(user);

        float radius = user.GetStatsRuntime.attackRange / user.transform.localScale.x * 2;
        user.GetAttackRadius.transform.localScale = new Vector3(radius, radius, 1);
    }
}
