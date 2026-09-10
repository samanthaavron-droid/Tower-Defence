using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsTemplate", menuName = "Scriptable Objects/Enemy Stats Template")]

public class EnemyStatsTemplate : ScriptableObject
{
    public float health;
    public float attackRange;
    public float projectileSpeed;
    public float rechargeTime; //between shots
    public float damage;
    public float speed;
    public float projectileHealth;
}
