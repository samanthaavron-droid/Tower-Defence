using UnityEngine;

public class BasicTowerProjectile : MonoBehaviour
{
    [HideInInspector] public BuildingStatsRuntime buildingStats;
    [HideInInspector] public Vector3 futurePos;
    void Update()
    {
        MoveToFuturePoint();
    }
    private void MoveToFuturePoint()
    {
        transform.position += futurePos * buildingStats.projectileSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<EnemyBase>() != null)
            {
                EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
                enemy.TakeDamage(buildingStats.damage);
                Destroy(gameObject);
            } else if (collision.gameObject.GetComponent<TargetedEnemyProjectile>() != null)
            {
                TargetedEnemyProjectile enemy = collision.gameObject.GetComponent<TargetedEnemyProjectile>();
                enemy.TakeDamage(buildingStats.damage);
                Destroy(gameObject);
            }
        }
    }
}
