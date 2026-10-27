using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private StatsRuntime statsRuntime;
    public StatsRuntime SetStatsRuntime { get { return statsRuntime; } set { statsRuntime = value; }  }
    private Vector3 futurePos;
    public Vector3 GetSetFuturePos { get { return futurePos; } set { futurePos = value; } }
    private GameObject target;
    public GameObject SetTarget { get { return target; } set { target = value; } }
    private Unit user;
    public Unit SetUser { set { user = value; } }
    private new TagHandle tag;
    public TagHandle SetTag { set { tag = value; } }
    private ParticleSystem hitEffect;
    public ParticleSystem SetHitEffect { set { hitEffect = value; } }
    
    void Start()
    {
        StartCoroutine(DelayDestroy());
    }

    void Update()
    {
        MoveToFuturePoint();
    }
    private void MoveToFuturePoint()
    {
        if (statsRuntime != null)
        {
            transform.position += futurePos * statsRuntime.projectileSpeed * Time.deltaTime;
        }
    }
    public void TakeDamage(float damage)
    {
        statsRuntime.projectileHealth -= damage;
        if (statsRuntime.projectileHealth <= 0)
        {
            if (user != null)
            {
                user.TakeDamage(1000);
                Destroy(gameObject);
            } else
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;

        if (target == null)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                Unit enemy = collision.gameObject.GetComponentInParent<Unit>();
                Projectile enemyPj = collision.gameObject.GetComponentInParent<Projectile>();
                if (enemy != null)
                {
                    HitEffect();
                    enemy.TakeDamage(statsRuntime.damage);
                    Destroy(gameObject);
                }
                else if (enemyPj != null)
                {
                    HitEffect();
                    enemyPj.TakeDamage(statsRuntime.damage);
                    Destroy(gameObject);
                }
            }
        } else if (target != null) //        if (collision.isTrigger) return;
        {
            if (collision.gameObject == target)
            {
                EnemyBase enemy = collision.gameObject.GetComponentInParent<EnemyBase>();
                BuildingBase building = collision.gameObject.GetComponentInParent<BuildingBase>();
                Projectile projectile = collision.gameObject.GetComponentInParent<Projectile>();

                if (enemy != null)
                {
                    HitEffect();
                    enemy.TakeDamage(statsRuntime.damage);
                } else if (building != null)
                {
                    HitEffect();
                    building.TakeDamage(statsRuntime.damage);
                    user.TakeDamage(1000f);
                } else if (projectile != null)
                {
                    HitEffect();
                    projectile.TakeDamage(statsRuntime.damage);
                }
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(10 * GlobalSettings.GetSetModifier);
        Destroy(gameObject);
    }
    private void HitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem newEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);

            newEffect.Play();
        }
    }
}
