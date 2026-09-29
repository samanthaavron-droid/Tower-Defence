using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public BuildingStatsRuntime buildingStats;
    public EnemyStatsRuntinme enemyStats;
    public Vector3 futurePos;
    public GameObject target;
    public EnemyBase user;
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
        if (buildingStats != null)
        {
            transform.position += futurePos * buildingStats.projectileSpeed * Time.deltaTime;
        } else if (enemyStats != null)
        {
            transform.position += futurePos * enemyStats.projectileSpeed * Time.deltaTime;
        }
    }
    public void TakeDamage(float damage)
    {
        if (buildingStats != null)
        {
            buildingStats.projectileHealth -= damage;
            if (buildingStats.projectileHealth <= 0)
            {
                Destroy(gameObject);
            }
        } else if (enemyStats != null)
        {
            enemyStats.projectileHealth -= damage;
            if (enemyStats.projectileHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target == null)
        {
            if (buildingStats != null)
            {
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
                    Projectile enemyPj = collision.gameObject.GetComponent<Projectile>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(buildingStats.damage);
                        Destroy(gameObject);
                    }
                    else if (enemyPj != null)
                    {
                        enemyPj.TakeDamage(buildingStats.damage);
                        Destroy(gameObject);
                    }
                }
            } 
            else if (enemyStats != null)
            {
                if (collision.gameObject.CompareTag("Building"))
                {
                    BuildingBase tower = collision.gameObject.GetComponent<BuildingBase>();
                    Projectile towerPj = collision.gameObject.GetComponent<Projectile>();
                    if (tower != null)
                    {
                        tower.TakeDamage(enemyStats.damage);
                        Destroy(gameObject);
                    }
                    else if (towerPj != null)
                    {
                        towerPj.TakeDamage(enemyStats.damage);
                        Destroy(gameObject);
                    }
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
                    enemy.TakeDamage(buildingStats.damage);
                } else if (building != null)
                {
                    building.TakeDamage(enemyStats.damage);
                    user.TakeDamage(1000f);
                } else if (projectile != null)
                {
                    if (user != null)
                    {
                        projectile.TakeDamage(enemyStats.damage);
                    } 
                    else if (user == null)
                    {
                        projectile.TakeDamage(buildingStats.damage);
                    }
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
