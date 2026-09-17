using UnityEngine;

public class TargetedTowerProjectile : MonoBehaviour
{
    [HideInInspector] public BuildingStatsRuntime buildingStats;
    [HideInInspector] public Vector3 futurePos;
    [HideInInspector] public GameObject target;
    void Update()
    {
        MoveToFuturePoint();
    }
    public void TakeDamage(float damage)
    {
        buildingStats.health -= damage;
        if (buildingStats.health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            enemy.TakeDamage(buildingStats.damage);
            Destroy(gameObject);
        }
    }
    private void MoveToFuturePoint()
    {
        transform.position += futurePos * buildingStats.projectileSpeed * Time.deltaTime;
    }
}
