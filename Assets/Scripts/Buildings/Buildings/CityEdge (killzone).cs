using UnityEngine;

public class CityEdge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();

            enemy.GetStatsRuntime.worth = enemy.GetStatsRuntime.worth * 0.5f;

            enemy.TakeDamage(10000);
        }
    }
}
