using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundClip;
    public float repeatInterval = 2f; // Tekrarlama aralýðý (örneðin her 2 saniyede bir)

    void Start()
    {
        // AudioSource'a ses dosyasýný ve loop özelliðini ayarla
        audioSource.clip = soundClip;
        audioSource.loop = true;

        // Sesin tekrarlanmasýný saðlamak için bir coroutine kullanabilirsiniz
        StartCoroutine(RepeatSound());
    }

    IEnumerator RepeatSound()
    {
        while (true)
        {
            // Ses dosyasýný baþlat
            audioSource.Play();

            // Belirli bir süre bekleyip tekrar baþlat
            yield return new WaitForSeconds(repeatInterval);
        }
    }
}
