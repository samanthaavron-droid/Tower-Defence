using UnityEngine;

public class Laser : WeaponLogic
{
    public Laser(StatsTemplate stats)
    {
        base.statsRuntime = new(stats);
    }
    public override void Attack(Unit user)
    {
        if (user == null) return;

        if (Input.GetMouseButton(1))
        {
            user.line.enabled = true;
            user.line.SetPosition(0, user.transform.position);

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            Vector3 direction = (mousePos - user.transform.position).normalized;

            RaycastHit2D hit = RaycastCheck(user, direction, statsRuntime, LayerMask.GetMask("Enemy"));

            if (hit == true)
            {
                user.line.SetPosition(1, hit.transform.position);

                if (statsRuntime.cooldown > 0) return;

                hit.collider.gameObject.GetComponent<Unit>().TakeDamage(statsRuntime.damage);

                statsRuntime.cooldown = statsRuntime.rechargeTime;
            } else
            {
                user.line.SetPosition(1, user.transform.position + (Vector3)(direction * statsRuntime.attackRange));
            }
        } else
        {
            user.line.enabled = false;
        }
    }
}
