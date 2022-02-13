using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] ParticleSystem comboParticles;

    private void OnEnable()
    {
        EventManager.StartListening("Play Hit Particle", (Vector3 pos) => EmitCoinParticle(hitParticles, pos));
        EventManager.StartListening("Play Combo Particle", (Vector3 pos) => EmitCoinParticle(comboParticles, pos));
    }



    public void EmitCoinParticle(ParticleSystem particles, Vector3 position) {


        particles.transform.position = position;

        particles.emission.SetBursts(new[] { new ParticleSystem.Burst(0.000f, 1) });

        particles.Play();
    }



}
