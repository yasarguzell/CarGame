using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spells : MonoBehaviour
{
    public Button myButton; // Buton referans�
    public GameObject car; // Araba objesi referans�    
    private Rigidbody carRigidbody; // Araban�n Rigidbody bile�eni
    public ParticleSystem flameParticle;
    private TMP_Text buttonText;
    public ParticleSystem explosionParticle;
    public ParticleSystem thunderParticle1;
    public ParticleSystem thunderParticle2;
    public ParticleSystem thunderParticle3;
    public ParticleSystem thunderParticle4;
    public ParticleSystem thunderParticle5;
    public float destroyRadius = 100f; // Yok edilecek d��manlar�n maksimum mesafesi


    void Start()
    {
        // Butonun onClick eventine metod ekleme
        myButton.onClick.AddListener(OnButtonClick);

        // Araban�n Rigidbody bile�enini al
        carRigidbody = car.GetComponent<Rigidbody>();
     }

    void OnButtonClick()
    {
        // Butonun Text bile�enine eri�im
        buttonText = myButton.GetComponentInChildren<TMP_Text>();

        if (buttonText.text == "Nitro")
        {
            // A��rl��� yar�ya d���r
            carRigidbody.mass /= 2;
            flameParticle.Play();
            myButton.interactable = false;
            StartCoroutine(ResetNitro(4));
            
        }
        else if (buttonText.text == "Explotion")
        {
            explosionParticle.Play();
            myButton.interactable = false;

            // T�m d��manlar� bul
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Karakterin konumu
            Vector3 playerPosition = transform.position;

            // Belirli mesafedeki d��manlar� yok et
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(playerPosition, enemy.transform.position);
                if (distance <= destroyRadius)
                {
                    Destroy(enemy);
                }
            }

            StartCoroutine(ResetExplotion(10));

        }
        else if (buttonText.text == "Thunder")
        {
            thunderParticle1.Play();
            thunderParticle2.Play();
            thunderParticle3.Play();
            thunderParticle4.Play();
            thunderParticle5.Play();

            myButton.interactable = false;

            // T�m d��manlar� bul
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Karakterin konumu
            Vector3 playerPosition = transform.position;

            // Belirli mesafedeki d��manlar� yok et
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(playerPosition, enemy.transform.position);
                if (distance <= destroyRadius)
                {
                    Destroy(enemy);
                }
            }

            StartCoroutine(ResetThunder(10));

        }

        else
        {
            Debug.Log("spell yok");
        }
       
    }
    IEnumerator ResetNitro(int cooldown)
    {
        yield return new WaitForSeconds(4);
        carRigidbody.mass *= 2;
        flameParticle.Stop();
        while (cooldown>0)
        {
            buttonText.text = cooldown.ToString();
            yield return new WaitForSeconds(1);
            cooldown -= 1;
        }
        yield return new WaitForSeconds(6);
        buttonText.text = "Nitro";
        myButton.interactable = true;
    }

    IEnumerator ResetExplotion(int cooldown)
    {
        while (cooldown > 0)
        {
            buttonText.text = cooldown.ToString();
            yield return new WaitForSeconds(1);
            cooldown -= 1;
        }                
        buttonText.text = "Explotion";        
        myButton.interactable = true;
    }

    IEnumerator ResetThunder(int cooldown)
    {
        while (cooldown > 0)
        {
            buttonText.text = cooldown.ToString();
            yield return new WaitForSeconds(1);
            cooldown -= 1;
        }
        buttonText.text = "Thunder";
        myButton.interactable = true;
    }
}
