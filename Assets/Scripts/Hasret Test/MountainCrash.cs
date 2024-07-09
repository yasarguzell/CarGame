using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainCrash : MonoBehaviour
{
    public GameObject _gameObject;
    public FuelSystem _fuelSystem;
    public ParticleSystem _particleSystem;
    public ParticleSystem particleSystem_;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _fuelSystem.currentFuel = 0; //aracın yakıtı bitti
            _particleSystem.Play(); // patlama efecti çıkar
            particleSystem_.startDelay = 2;
            particleSystem_.Play();
        }
    }
}
