using UnityEngine;

[CreateAssetMenu(fileName = "StatsTemplate", menuName = "Scriptable Objects/Stats Template")]

public class StatsTemplate : ScriptableObject
{
    public float health;
    public float attackRange;
    public float projectileSpeed;
    public float damage;
    public float projectileHealth;
    public float rechargeTime;
    public float debreeSize;
    public float buildingSpeed;
    public bool priority;
    public float speed;
    public float worth;
}
