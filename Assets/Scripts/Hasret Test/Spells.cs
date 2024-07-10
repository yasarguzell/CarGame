using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spells : MonoBehaviour
{
    public Button myButton; // Buton referansý
    public GameObject car; // Araba objesi referansý    
    private Rigidbody carRigidbody; // Arabanýn Rigidbody bileþeni
    public ParticleSystem flameParticle;
    private Text buttonText;
    public ParticleSystem explosionParticle;
    public ParticleSystem thunderParticle1;
    public ParticleSystem thunderParticle2;
    public ParticleSystem thunderParticle3;
    public ParticleSystem thunderParticle4;
    public ParticleSystem thunderParticle5;
    public float destroyRadius = 100f; // Yok edilecek düþmanlarýn maksimum mesafesi


    void Start()
    {
        // Butonun onClick eventine metod ekleme
        myButton.onClick.AddListener(OnButtonClick);

        // Arabanýn Rigidbody bileþenini al
        carRigidbody = car.GetComponent<Rigidbody>();
     }

    void OnButtonClick()
    {
        // Butonun Text bileþenine eriþim
        buttonText = myButton.GetComponentInChildren<Text>();

        if (buttonText.text == "Nitro")
        {
            // Aðýrlýðý yarýya düþür
            carRigidbody.mass /= 2;
            flameParticle.Play();
            myButton.interactable = false;
            StartCoroutine(ResetNitro(10));
            
        }
        else if (buttonText.text == "Explotion")
        {
            explosionParticle.Play();
            myButton.interactable = false;

            // Tüm düþmanlarý bul
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Karakterin konumu
            Vector3 playerPosition = transform.position;

            // Belirli mesafedeki düþmanlarý yok et
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

            // Tüm düþmanlarý bul
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Karakterin konumu
            Vector3 playerPosition = transform.position;

            // Belirli mesafedeki düþmanlarý yok et
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
        
        while (cooldown>0)
        {
            buttonText.text = cooldown.ToString();
            yield return new WaitForSeconds(1);
            cooldown -= 1;
        }

        // Aðýrlýðý eski haline getir
        carRigidbody.mass *= 2;
        buttonText.text = "Nitro";
        flameParticle.Stop();
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
