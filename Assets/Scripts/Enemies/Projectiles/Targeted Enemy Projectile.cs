using UnityEngine;

public class TargetedEnemyProjectile : MonoBehaviour
{
    [HideInInspector] public EnemyStatsRuntinme enemyStats;
    [HideInInspector] public Vector3 futurePos;
    [HideInInspector] public GameObject target = null;
    private void Update()
    {
        if (target == null) return;

        MoveToFuturePoint();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            BuildingBase building = collision.gameObject.GetComponent<BuildingBase>();
            building.TakeDamage(enemyStats.damage);
            Debug.Log("ROCKET");
            GetComponent<EnemyBase>().TakeDamage(1000);
        }
    }
    private void MoveToFuturePoint()
    {
        transform.position += futurePos * enemyStats.projectileSpeed * Time.deltaTime;
    }
}
