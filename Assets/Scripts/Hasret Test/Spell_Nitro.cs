using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spell_Nitro : MonoBehaviour
{

    public Button myButton;
    void Start()
    {
        // "MyButton" adında bir buton bul
        GameObject buttonObject = GameObject.Find("Spell");

        if (buttonObject != null)
        {
            Button button = buttonObject.GetComponent<Button>();
            if (button != null)
            {
                myButton = button;
            }
        }
    }
    void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);    //yok et
            myButton.GetComponentInChildren<TMP_Text>().text = "Nitro";

            }
        }


    }
