using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedParticleControl : MonoBehaviour
{
    public ParticleSystem speedParticle; // Particle sistemini burada tanımlıyoruz
    public float speedThreshold = 5f; // Partiküllerin ortaya çıkacağı minimum hız
    public float maxEmissionRate = 50f; // Maksimum partikül üretim oranı

    private Rigidbody rb;
    private ParticleSystem.EmissionModule emissionModule;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Arabanın Rigidbody bileşenini al
        emissionModule = speedParticle.emission; // Particle sisteminin Emission modülünü al
    }

    void Update()
    {
        float speed = rb.velocity.magnitude; // Aracın hızını hesapla

        if (speed > speedThreshold)
        {
            // Hız belirli bir eşiği geçtiğinde partikülleri etkinleştir ve üretim oranını hıza göre ayarla
            speedParticle.Play();
            emissionModule.rateOverTime = Mathf.Lerp(0, maxEmissionRate, (speed - speedThreshold) / speedThreshold);
        }
        else
        {
            // Hız eşiğin altındaysa partikülleri durdur
            speedParticle.Stop();
        }
    }
}
