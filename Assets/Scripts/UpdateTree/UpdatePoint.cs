using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatePoint : MonoBehaviour
{
    public void GetUpdate(CarAttributes carAttributes)
    {
        if (transform.GetChild(0).GetComponent<TMP_Text>().text == "Speed") { carAttributes.Speed++; }
        if (transform.GetChild(0).GetComponent<TMP_Text>().text == "HP") { carAttributes.HP++; }
        if (transform.GetChild(0).GetComponent<TMP_Text>().text == "Defense") { carAttributes.Defense++; }
        if (transform.GetChild(0).GetComponent<TMP_Text>().text == "GunDamage") { carAttributes.GunDamage++; }
        if (transform.GetChild(0).GetComponent<TMP_Text>().text == "GunShootingSpeed") { carAttributes.GunShootingSpeed++; }
        if (transform.GetChild(0).GetComponent<TMP_Text>().text == "ChargerCapacity") { carAttributes.ChargerCapacity++; }

        transform.parent.parent.gameObject.SetActive(false);
    }
}
