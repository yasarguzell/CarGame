using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Spell_Explotion : MonoBehaviour
{
    public Button myButton;

    void Start()
    {
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
            myButton.GetComponentInChildren<TMP_Text>().text = "Explotion";

        }
    }
}
