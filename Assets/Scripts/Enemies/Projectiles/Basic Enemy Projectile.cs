using UnityEngine;

public class BasicEnemyProjectile : MonoBehaviour
{
    [HideInInspector] public EnemyStatsRuntinme enemyStats;
    private void Update()
    {
        Move();
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
    private void Move()
    {
        transform.position += Vector3.left * enemyStats.projectileSpeed * Time.deltaTime;
    }
}
