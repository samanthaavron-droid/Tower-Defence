using UnityEngine;

public class BasicEnemyProjectile : MonoBehaviour
{
    [HideInInspector] public EnemyStatsRuntinme enemyStats;
    [HideInInspector] public Vector3 futurePos;
    private void Update()
    {
        MoveToFuturePoint();
    }
    public void TakeDamage(float damage)
    {
        enemyStats.projectileHealth -= damage;
        if (enemyStats.projectileHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Building"))
        {
            BuildingBase building = collision.gameObject.GetComponent<BuildingBase>();
            building.TakeDamage(enemyStats.damage);
            Destroy(gameObject);
        }
    }
    private void MoveToFuturePoint()
    {
        transform.position += futurePos * enemyStats.projectileSpeed * Time.deltaTime;
    }
}
