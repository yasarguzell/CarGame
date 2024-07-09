using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatePoint : MonoBehaviour
{
    public void GetUpdate(CarAttributes carAttributes)
    {
        if (GetComponentInChildren<TMP_Text>().text == "Speed") { carAttributes.speedIndex++; }
        if (GetComponentInChildren<TMP_Text>().text == "HP") { carAttributes.hpIndex++; }
        if (GetComponentInChildren<TMP_Text>().text == "Defense") { carAttributes.defenseIndex++; }
        if (GetComponentInChildren<TMP_Text>().text == "GunDamage") { carAttributes.gunDamageIndex++; }
        if (GetComponentInChildren<TMP_Text>().text == "GunShootingSpeed") { carAttributes.gunShootingSpeedIndex++; }
        if (GetComponentInChildren<TMP_Text>().text == "ChargerCapacity") { carAttributes.chargerCapacityIndex++; }

        this.gameObject.SetActive(false);
    }
}
