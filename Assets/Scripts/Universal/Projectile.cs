using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public StatsRuntime statsRuntime;
    public Vector3 futurePos;
    public GameObject target;
    public Unit user;
    public new TagHandle tag;
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
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target == null)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                Unit enemy = collision.gameObject.GetComponent<Unit>();
                Projectile enemyPj = collision.gameObject.GetComponent<Projectile>();
                if (enemy != null)
                {
                    enemy.TakeDamage(statsRuntime.damage);
                    Destroy(gameObject);
                }
                else if (enemyPj != null)
                {
                    enemyPj.TakeDamage(statsRuntime.damage);
                    Destroy(gameObject);
                }
            }
        } else if (target != null)
        {
            if (collision.gameObject == target)
            {
                EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
                BuildingBase building = collision.gameObject.GetComponent<BuildingBase>();
                Projectile projectile = collision.gameObject.GetComponent<Projectile>();

                if (enemy != null)
                {
                    enemy.TakeDamage(statsRuntime.damage);
                } else if (building != null)
                {
                    building.TakeDamage(statsRuntime.damage);
                    user.TakeDamage(1000f);
                } else if (projectile != null)
                {
                    projectile.TakeDamage(statsRuntime.damage);
                }
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(10 * GlobalSettings.modifier);
        Destroy(gameObject);
    }
}
