using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spell_Nitro : MonoBehaviour
{

    public Button myButton;
    void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);    //yok et
            myButton.GetComponentInChildren<TMP_Text>().text = "Nitro";

            }
        }


    }
