using UnityEngine;

public class WeaponLogic //this is the class on which weapons are built
{
    public EnemyStatsTemplate enemyStatsTemplate;
    public EnemyStatsRuntinme enemyStats;
    public BuildingStatsTemplate buildingStatsTemplate;
    public BuildingStatsRuntime buildingStats;

    public virtual void Attack(EnemyBase user) { }
    public virtual void Attack(BuildingBase user) { }
    public void ProjectileData(GameObject projectile, EnemyStatsRuntinme enemyStats, Vector3 futurePos, EnemyBase user = null, GameObject target = null)
    {
        Projectile projectileInfo = projectile.GetComponent<Projectile>();

        projectileInfo.enemyStats = enemyStats;
        projectileInfo.futurePos = futurePos;
        projectile.transform.up = futurePos;
        projectileInfo.user = user;
        projectileInfo.target = target;
    }
    public void ProjectileData(GameObject projectile, BuildingStatsRuntime buildingStats, Vector3 futurePos, GameObject target = null)
    {
        Projectile projectileInfo = projectile.GetComponent<Projectile>();

        projectileInfo.buildingStats = buildingStats;
        projectileInfo.futurePos = futurePos;
        projectile.transform.up = futurePos;
        projectileInfo.target = target;
    }
    public RaycastHit2D RaycastCheck(EnemyBase user, Vector2 direction, EnemyStatsRuntinme enemyStats, LayerMask targetLayer)
    {
        RaycastHit2D hit = Physics2D.Raycast(user.transform.position, direction, enemyStats.attackRange, targetLayer);
        return hit;
    }
    public RaycastHit2D RaycastCheck(BuildingBase user, Vector2 direction, BuildingStatsRuntime buildingStats, LayerMask targetLayer)
    {
        RaycastHit2D hit = Physics2D.Raycast(user.transform.position, direction, buildingStats.attackRange, targetLayer);
        return hit;
    }
    public Collider2D OverlapPointCheck(EnemyBase user, LayerMask targetMask)
    {
        Collider2D hit = Physics2D.OverlapPoint(user.transform.position, targetMask);
        return hit;
    }
    public Collider2D OverlapPointCheck(BuildingBase user, LayerMask targetMask)
    {
        Collider2D hit = Physics2D.OverlapPoint(user.transform.position, targetMask);
        return hit;
    }
    public Collider2D OverlapCircleCheck(EnemyBase user, EnemyStatsRuntinme enemyStats, LayerMask targetLayer)
    {
        Collider2D hit = Physics2D.OverlapCircle(user.transform.position, enemyStats.attackRange, targetLayer);
        return hit;
    }
    public Collider2D OverlapCircleCheck(BuildingBase user, BuildingStatsRuntime buildingStats, LayerMask targetLayer)
    {
        Collider2D hit = Physics2D.OverlapCircle(user.transform.position, buildingStats.attackRange, targetLayer);
        return hit;
    }
    public Collider2D OverlapCircleAllCheck(EnemyBase user, EnemyStatsRuntinme enemyStats, LayerMask targetLayer)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(user.transform.position, enemyStats.attackRange, targetLayer);

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
    public Collider2D OverlapCircleAllCheck(BuildingBase user, BuildingStatsRuntime buildingStats, LayerMask targetLayer)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(user.transform.position, buildingStats.attackRange, targetLayer);

        if (hits.Length > 0)
        {
            int randomTarget = Random.Range(0, hits.Length);
            Collider2D hit = hits[randomTarget];

            return hit;
        }
        else
        {
            return null;
        }
    }
}
