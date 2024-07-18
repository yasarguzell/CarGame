using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KmCount : MonoBehaviour
{
    public Transform vehicleTransform; // Aracın Transform bileşeni
    public Text odometerText; // Kilometre sayacını gösterecek Text elemanı
    private Vector3 startPosition; // Başlangıç pozisyonu
    private float totalDistance = 0f; // Toplam mesafe
    private bool isInitialized = false; // İlk frame kontrolü

    void Start()
    {
        // Başlangıç pozisyonunu kaydet
        startPosition = RoundVector3(vehicleTransform.position);
    }

    void Update()
    {
        // İlk frame'de mesafe hesaplamasını atla
        if (!isInitialized)
        {
            isInitialized = true;
            return;
        }

        // Şu anki pozisyonu al ve yuvarla
        Vector3 currentPosition = RoundVector3(vehicleTransform.position);

        // Başlangıç pozisyonundan şu anki pozisyona olan mesafeyi hesapla
        float distanceFromStart = Vector3.Distance(currentPosition, startPosition);

        // Eğer şu anki mesafe toplam mesafeden büyükse, toplam mesafeyi güncelle
        if (distanceFromStart > totalDistance)
        {
            totalDistance = distanceFromStart;
        }

        // Kilometre sayacını güncelle
        // odometerText.text = totalDistance.ToString("F2") + " km";
        //CoreUISignals.Instance.onGameScoreTextUpdate((int)totalDistance);
    }

    // Vector3'ü yuvarlama fonksiyonu
    private Vector3 RoundVector3(Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    }
}
