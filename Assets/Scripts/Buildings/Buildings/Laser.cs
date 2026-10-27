using UnityEngine;

public class Laser : WeaponLogic
{
    private Collider2D lastHit;
    public Laser(StatsTemplate stats)
    {
        base.statsRuntime = new(stats);
    }
    public override void Attack(Unit user)
    {
        if (user == null) return;

        if (Input.GetMouseButton(1))
        {
            user.GetLine.enabled = true;
            user.GetLine.SetPosition(0, user.transform.position);

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            Vector3 direction = (mousePos - user.transform.position).normalized;

            LaserFireEffect(user, new Vector2(direction.x, direction.y));

            RaycastHit2D hit = RaycastCheck(user, direction, statsRuntime, LayerMask.GetMask("Enemy"));

            if (hit == true)
            {
                user.GetLine.SetPosition(1, hit.point);

                LaserHitEffect(user, hit);

                if (statsRuntime.cooldown > 0) return;

                Unit unit = hit.collider.gameObject.GetComponent<Unit>();
                Projectile proj = hit.collider.gameObject.GetComponent<Projectile>();

                if (unit != null) unit.TakeDamage(statsRuntime.damage);
                else if  (proj != null) proj.TakeDamage(statsRuntime.damage);

                statsRuntime.cooldown = statsRuntime.rechargeTime;
            } else
            {
                lastHit = null;

                user.GetLine.SetPosition(1, mousePos);

                if (user.GetHitEffect.isPlaying == true)
                    user.GetHitEffect.Stop();
            }
        } else
        {
            lastHit = null;

            user.GetLine.enabled = false;
            user.GetFireEffect.Stop();
            user.GetHitEffect.Stop();
        }
    }
    private void LaserFireEffect(Unit user, Vector2 direction)
    {
        user.GetFireEffect.gameObject.transform.up = -direction;

        FireEffectPlay(user);
    }
    private void LaserHitEffect(Unit user, RaycastHit2D hit)
    {
        user.GetHitEffect.transform.position = hit.point;

        if (lastHit != hit.collider)
        {
            user.GetHitEffect.Stop();
            user.GetHitEffect.Play();

            lastHit = hit.collider;
        } else if (user.GetHitEffect.isPlaying == false)
        {
            user.GetHitEffect.Play();
        }
    }
}
