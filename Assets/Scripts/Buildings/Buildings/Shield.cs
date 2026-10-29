using UnityEngine;
public class Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyBase enemy = collision.GetComponent<EnemyBase>();

            if (enemy == null)
            {
                Projectile projectile = collision.GetComponent<Projectile>();
                projectile.SetStatsRuntime.projectileSpeed = projectile.SetStatsRuntime.projectileSpeed * 0.5f;
            }
            else
            {
                enemy.GetStatsRuntime.speed = enemy.GetStatsRuntime.speed * 0.5f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyBase enemy = collision.GetComponent<EnemyBase>();

            if (enemy == null)
            {
                Projectile projectile = collision.GetComponent<Projectile>();
                projectile.SetStatsRuntime.projectileSpeed = projectile.SetStatsRuntime.projectileSpeed * 2f;
            } else
            {
                enemy.GetStatsRuntime.speed = enemy.GetStatsRuntime.speed * 2f;
            }
        }
    }
}
