using UnityEngine;
using System.Collections.Generic;


public class Bomber : WeaponLogic
{
    private List<GameObject> alreadyBombed = new();
    public Bomber(StatsTemplate stats)
    {
        base.statsRuntime= new(stats);
    }
    public override void Attack(Unit user)
    {
        if (statsRuntime.cooldown > 0 || user == null) return;

        Collider2D hit = OverlapPointCheck(user, LayerMask.GetMask("Building"));
        if (hit != null)
        {
            if (alreadyBombed.Contains(hit.gameObject) == true) return; //making sure the building isn't bombed twice

            GameObject projectile = GameObject.Instantiate(user.projectilePrefab, user.transform.position, Quaternion.identity);

            ProjectileData(projectile, statsRuntime, user.transform.position, TagHandle.GetExistingTag("Building"), user);

            alreadyBombed.Add(hit.gameObject);
            //explosion effect
            statsRuntime.cooldown = statsRuntime.rechargeTime;
        }
    }
}
