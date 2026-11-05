using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Rocket : WeaponLogic
{
    private GameObject rocket;
    private Unit useruser;
    public Rocket(StatsTemplate stats)
    {
        base.statsRuntime= new(stats);
    }
    public override void Attack(Unit user)
    {
        if (statsRuntime.cooldown > 0 || user == null) return;

        Collider2D hit = OverlapCircleAllCheck(user, statsRuntime, LayerMask.GetMask("Building"));

        if (hit != null)
        {
            rocket = GameObject.Instantiate(user.GetProjectilePrefab, user.transform.position, Quaternion.identity);

            ProjectileData(rocket, statsRuntime, (hit.transform.position - user.transform.position).normalized, TagHandle.GetExistingTag("Building"), user.GetHitEffect, user, hit.gameObject);

            statsRuntime.cooldown = statsRuntime.rechargeTime;

            useruser = user;

            user.StartCoroutine(CheckRocket());
        }
    }
    private IEnumerator CheckRocket()
    {
        while (rocket != null)
        {
            yield return new WaitForSeconds(1f);
        }

        if (useruser != null)
            useruser.TakeDamage(1000f);
    }
}
