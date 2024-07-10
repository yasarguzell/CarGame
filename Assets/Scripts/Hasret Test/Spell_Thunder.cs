using UnityEngine;
using UnityEngine.UI;


public class Spell_Thunder : MonoBehaviour
{
    public Button myButton;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);    //yok et
            myButton.GetComponentInChildren<Text>().text = "Thunder";

        }
    }

}
