using UnityEngine;

public class RocketTrail : MonoBehaviour
{
    [SerializeField] private Transform target;
    private ParticleSystem particle;
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        transform.SetParent(null);
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = target.transform.position;
        } else
        {
            if (particle.isPlaying)
            {
                particle.Stop();
                Destroy(gameObject, 3f);
            }
        }
    }
}
