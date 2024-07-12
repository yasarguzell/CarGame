using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    public AudioSource engineAudio;
    public float minPitch = 0.8f;
    public float maxPitch = 1.5f;
    public float maxSpeed = 100f;
    private Rigidbody carRigidbody;

    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        engineAudio.Play();
    }

    void Update()
    {
        float speed = carRigidbody.velocity.magnitude;
        float pitch = Mathf.Lerp(minPitch, maxPitch, speed / maxSpeed);
        engineAudio.pitch = pitch;
    }
}
