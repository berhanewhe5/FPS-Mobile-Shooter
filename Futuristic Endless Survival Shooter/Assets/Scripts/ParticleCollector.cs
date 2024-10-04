using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollector : MonoBehaviour
{
    // Reference to the ParticleSystem
    public ParticleSystem ps;

    // Reference to the points bar script
    public pointsBar pointsBar;

    // Array to store particles
    private ParticleSystem.Particle[] particles;

    // Reference to the player's transform
    public Transform player;

    // Distance within which particles are collected
    public float removalDistance = 0.5f; // Adjust as needed

    void Start()
    {
        // Get the ParticleSystem component attached to this GameObject
        ps = GetComponent<ParticleSystem>();

        // Initialize the particles array with the maximum number of particles the system can have
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    // Update is called once per frame
    void Update()
    {
        // Get the number of particles alive in the system
        int particlesAlive = ps.GetParticles(particles);

        // Process each particle
        for (int i = 0; i < particlesAlive; i++)
        {
            ParticleSystem.Particle p = particles[i];
            Vector3 particlePosition = ps.transform.TransformPoint(p.position);
            float distanceToPlayer = Vector3.Distance(particlePosition, player.position);
            if (distanceToPlayer < removalDistance)
            {
                // Increase the points
                pointsBar.points += 1;
                player.gameObject.GetComponent<PlayerShooting>().ammoAvailable += 1;
                // Remove the particle by setting its remaining lifetime to zero
                p.remainingLifetime = 0;
                Debug.Log("Particle Collected");
            }

            // Update the particle in the array
            particles[i] = p;
        }

        // Set the updated particles back to the ParticleSystem
        ps.SetParticles(particles, particlesAlive);
    }
}
