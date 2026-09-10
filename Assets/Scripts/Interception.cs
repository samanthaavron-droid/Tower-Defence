using UnityEngine;

public class Interception //stolen formula
{
    public static Vector3 GetInterceptionPoint(Vector3 shooterPos, Vector3 targetPos, Vector3 targetVelocity, float projectileSpeed)
    {
        Vector2 displacement = targetPos - shooterPos;

        // quadratic equation coefficients
        float a = targetVelocity.sqrMagnitude - (projectileSpeed * projectileSpeed);
        float b = 2f * Vector2.Dot(displacement, targetVelocity);
        float c = displacement.sqrMagnitude;

        // if discriminant is negative, the target is too fast to intercept
        float discriminant = (b * b) - (4f * a * c);
        if (discriminant < 0f) return targetPos;

        float sqrtD = Mathf.Sqrt(discriminant);

        // calculating both possible intersection times
        float t1 = (-b + sqrtD) / (2f * a);
        float t2 = (-b - sqrtD) / (2f * a);

        // finding the smallest positive time
        float interceptTime = -1f;
        if (t1 > 0f && t2 > 0f) interceptTime = Mathf.Min(t1, t2);
        else if (t1 > 0f) interceptTime = t1;
        else if (t2 > 0f) interceptTime = t2;

        // if a valid time is found, calculate the future position
        if (interceptTime > 0f)
        {
            return targetPos + (targetVelocity * interceptTime);
        }

        // fallback if no valid interception point
        return targetPos;
    }
}