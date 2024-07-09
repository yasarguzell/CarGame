using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek hedef
    public float smoothSpeed = 0.125f; // Lerp için hýz faktörü
    public Vector3 offset; // Kameranýn hedefe olan offset'i

    void Start()
    {
        // Ýzometrik bakýþ açýsý için offset belirle
        offset = new Vector3(10, 10, -10);
        // Kameranýn baþlangýç rotasyonunu hedefe doðru bakacak þekilde ayarla
        transform.rotation = Quaternion.Euler(30, 45, 0); // 30 derece yukarýdan, 45 derece yandan
    }

    void LateUpdate()
    {
        // Ýstenilen pozisyonu hesapla
        Vector3 desiredPosition = target.position + offset;
        // Mevcut pozisyon ile istenilen pozisyon arasýnda lerp uygula
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Kamerayý yeni pozisyona taþý
        transform.position = smoothedPosition;

        // Kameranýn hedefe bakmasýný saðla
        transform.LookAt(target);
    }
}
