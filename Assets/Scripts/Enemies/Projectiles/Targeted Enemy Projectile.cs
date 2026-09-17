using UnityEngine;

public class TargetedEnemyProjectile : MonoBehaviour
{
    [HideInInspector] public EnemyStatsRuntinme enemyStats;
    [HideInInspector] public Vector3 futurePos;
    [HideInInspector] public GameObject target;
    [HideInInspector] public EnemyBase user;
    private void Update()
    {
        MoveToFuturePoint();
    }
    public void TakeDamage(float damage)
    {
        enemyStats.projectileHealth -= damage;
        if (enemyStats.projectileHealth <= 0)
        {
            user.TakeDamage(1000);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            BuildingBase building = collision.gameObject.GetComponent<BuildingBase>();
            building.TakeDamage(enemyStats.damage);
            user.TakeDamage(1000);
            Destroy(gameObject);
        }
    }
    private void MoveToFuturePoint()
    {
        transform.position += futurePos * enemyStats.projectileSpeed * Time.deltaTime;
    }
}
