using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTest : MonoBehaviour
{
    public Transform target; // Hedef nesne
    public float speed = 5f; // Hareket hýzý
    public float rotationSpeed = 360f; // Dönüþ hýzý (derece/saniye)
    public float stopDistance = 1f; // Durma mesafesi (metre)

    void Update()
    {
        // Hedef pozisyonu al
        Vector3 targetPosition = target.position;

        // Hedefe olan mesafeyi hesapla
        float distance = Vector3.Distance(transform.position, targetPosition);

        // Hedefe belirli bir mesafeden yaklaþtýysa dur
        if (distance > stopDistance)
        {
            // Nesneyi hedefe doðru hareket ettir
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Hedef yönüne doðru bak
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Hasar Veriyoruz þuanda");
        }
    }
}
