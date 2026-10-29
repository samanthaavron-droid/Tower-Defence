using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Jet : WeaponLogic
{
    public Jet(StatsTemplate stats)
    {
        base.statsRuntime= new(stats);
    }
    public override void Attack(Unit user)
    {
        if (statsRuntime.cooldown > 0 || user == null) return;

        RaycastHit2D hit = RaycastCheck(user, Vector2.left, statsRuntime, LayerMask.GetMask("Building"));

        if (hit.collider == true)
        {
            user.StartCoroutine(FireBurst(user, (new Vector3(hit.point.x, hit.point.y, 0) - user.transform.position).normalized));

            user.StartCoroutine(SmoothMove((user)));  

            statsRuntime.cooldown = statsRuntime.rechargeTime;
        }
    }
    private IEnumerator FireBurst(Unit user, Vector3 direction)
    {
        for (int i = 0; i < 3; i++)
        {
            if (user == null) yield break;

            GameObject projectile = GameObject.Instantiate(user.GetProjectilePrefab, user.transform.position, Quaternion.identity);
            ProjectileData(projectile, statsRuntime, direction, TagHandle.GetExistingTag("Building"), user.GetHitEffect, null, null);

            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator SmoothMove(Unit user)
    {
        if (user.transform.position.y + 1.5f <= 2.25f)
        {
            float newPos = user.transform.position.y + 1.5f;
            while (user.transform.position.y != newPos)
            {
                float newY = Mathf.MoveTowards(user.transform.position.y, newPos, statsRuntime.speed * Time.deltaTime);

                user.transform.position = new Vector3(user.transform.position.x, newY, user.transform.position.z);

                yield return null;
            }
        }
        else if (user.transform.position.y - 1.5f >= -2.25f)
        {
            float newPos = user.transform.position.y - 1.5f;
            while (user.transform.position.y != newPos)
            {
                float newY = Mathf.MoveTowards(user.transform.position.y, newPos, statsRuntime.speed * Time.deltaTime);

                user.transform.position = new Vector3(user.transform.position.x, newY, user.transform.position.z);

                yield return null;
            }
        }
    }
}
