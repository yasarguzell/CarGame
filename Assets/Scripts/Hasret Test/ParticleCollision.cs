using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particleeffect : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private ParticleSystem.CollisionModule collisionModule;

    void Start()
    {
        // Çarpışma modülünü etkinleştir
        collisionModule = particleSystem.collision;
        collisionModule.enabled = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Çarpışma anında particleri başlat
        if (collision.gameObject.CompareTag("Player")) 
        {
            particleSystem.Play();
        }
            
    }

    void OnCollisionExit(Collision collision)
    {
        // Çarpışma sona erdiğinde particleri durdur
        particleSystem.Stop();
    }
}
