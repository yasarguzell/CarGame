using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Spell_Thunder : MonoBehaviour
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
            myButton.GetComponentInChildren<TMP_Text>().text = "Thunder";

        }
    }

}
