using UnityEngine;

public class WeaponLogic //this is the class on which weapons are built
{
    public StatsTemplate statsTemplate;
    public StatsRuntime statsRuntime;
    public virtual void Attack(Unit user) { }
    public void ProjectileData(GameObject projectile, StatsRuntime statsRuntime, Vector3 futurePos, TagHandle tag, Unit user = null, GameObject target = null)
    {
        Projectile projectileInfo = projectile.GetComponent<Projectile>();

        projectileInfo.statsRuntime = statsRuntime;
        projectileInfo.futurePos = futurePos;
        projectile.transform.up = futurePos;
        projectileInfo.user = user;
        projectileInfo.target = target;
        projectileInfo.tag = tag;
    }
    public RaycastHit2D RaycastCheck(Unit user, Vector2 direction, StatsRuntime statsRuntime, LayerMask targetLayer)
    {
        RaycastHit2D hit = Physics2D.Raycast(user.transform.position, direction, statsRuntime.attackRange, targetLayer);
        return hit;
    }
    public Collider2D OverlapPointCheck(Unit user, LayerMask targetMask)
    {
        Collider2D hit = Physics2D.OverlapPoint(user.transform.position, targetMask);
        return hit;
    }
    public Collider2D OverlapCircleCheck(Unit user, StatsRuntime statsRuntime, LayerMask targetLayer)
    {
        Collider2D hit = Physics2D.OverlapCircle(user.transform.position, statsRuntime.attackRange, targetLayer);
        return hit;
    }
    public Collider2D OverlapCircleAllCheck(Unit user, StatsRuntime statsRuntime, LayerMask targetLayer)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(user.transform.position, statsRuntime.attackRange, targetLayer);

        if (hits.Length > 0)
        {
            int randomTarget = Random.Range(0, hits.Length);
            Collider2D hit = hits[randomTarget];

            return hit;
        } else
        {
            return null;
        }
    }
    public bool OnScreen(Vector2 pos)
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(pos);
        if (viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1)
        {
            return true;
        }
        else return false;
    }
    public virtual void LevelUpTower() //Unit user
    {
        statsRuntime.health = statsRuntime.health * 1.5f;
        statsRuntime.attackRange = statsRuntime.attackRange * 1.25f;
        statsRuntime.currentLevel += 1;
        statsRuntime.debreeSize = statsRuntime.debreeSize * 1.25f;
        statsRuntime.rechargeTime = statsRuntime.rechargeTime * 0.75f;
        statsRuntime.worth = statsRuntime.worth * 1.5f;
    }
}
