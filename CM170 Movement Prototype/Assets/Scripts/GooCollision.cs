using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooCollision : MonoBehaviour
{
    ParticleSystem ps;
    public List<Color32> colors;
    public List<float> frictions;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleTrigger()
    {

        List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
        ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles, out ParticleSystem.ColliderData data);

        for (int i = 0; i < particles.Count; i++)
        {
            var p = particles[i];

            int currIndex = (int)p.rotation;

            if (currIndex < colors.Count)
            {
                p.rotation = currIndex + 1;
                p.startColor = colors[currIndex];
            } else {
                p.remainingLifetime = 0;
            }

            float currentFriction = 1;
            if (currIndex < frictions.Count)
            {
                currentFriction = frictions[currIndex];
            }

            
            particles[i] = p;
            for (int j = 0; j < data.GetColliderCount(i); j++)
            {
                var collider = data.GetCollider(i, j);
                if (collider.TryGetComponent(out Rigidbody2D rb))
                {
                    rb.velocity *= currentFriction;
                }
            }
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }
}
