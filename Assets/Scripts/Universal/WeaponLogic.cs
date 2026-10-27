using UnityEngine;

public class WeaponLogic //this is the class on which weapons are built
{
    protected StatsRuntime statsRuntime;
    public StatsRuntime SetGetStatsRuntime { get { return statsRuntime; } set { statsRuntime = value; } }
    private Collider2D[] overlapCheck = new Collider2D[10];
    public virtual void Attack(Unit user) { }
    public void ProjectileData(GameObject projectile, StatsRuntime statsRuntime, Vector3 futurePos, TagHandle tag, ParticleSystem particleSystem, Unit user = null, GameObject target = null)
    {
        Projectile projectileInfo = projectile.GetComponent<Projectile>();

        projectileInfo.SetStatsRuntime = statsRuntime;
        projectileInfo.GetSetFuturePos = futurePos;
        projectile.transform.up = futurePos;
        projectileInfo.SetUser = user;
        projectileInfo.SetTarget = target;
        projectileInfo.SetTag = tag;
        projectileInfo.SetHitEffect = particleSystem;
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
    public Collider2D OverlapMouseCheck(Unit user, Vector3 pos, LayerMask targetMask)
    {
        Collider2D hit = Physics2D.OverlapPoint(pos, targetMask);
        return hit;
    }
    public Collider2D OverlapCircleCheck(Unit user, StatsRuntime statsRuntime, LayerMask targetLayer)
    {
        int hit = Physics2D.OverlapCircleNonAlloc(user.transform.position, statsRuntime.attackRange, overlapCheck, targetLayer);

        Collider2D closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < hit; i++)
        {
            float distance = Vector2.Distance(user.transform.position, overlapCheck[i].transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = overlapCheck[i];
            }
        }
        return closestEnemy;
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
    public virtual void LevelUpTower(Unit user) //Unit user
    {
        statsRuntime.health = statsRuntime.health * 2f;
        statsRuntime.attackRange = statsRuntime.attackRange * 1.25f;
        statsRuntime.currentLevel += 1;
        statsRuntime.debreeSize = statsRuntime.debreeSize * 1.25f;
        statsRuntime.rechargeTime = statsRuntime.rechargeTime * 0.75f;
        statsRuntime.worth = statsRuntime.worth * 1.5f;
        statsRuntime.damage = statsRuntime.damage * 1.25f;
    }
    public virtual void FireEffectPlay(Unit user)
    {
        if (user.GetFireEffect == null) return;

        if (user.GetFireEffect.isPlaying == false)
            user.GetFireEffect.Play();
    }
    public virtual void FireEffectEmit(Unit user, int emit)
    {
        if (user.GetFireEffect == null) return;

        user.GetFireEffect.Emit(emit);
    }
}
