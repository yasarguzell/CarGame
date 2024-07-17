using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePointCollection : MonoBehaviour
{
    public GameObject carUpdatePanel;
    public GameObject gunUpdatePanel;
    public GameObject gunSlotPanel;

    public CarAttributes carAttributes;
    public GunAttributes gunAttributes;

    public List<GameObject> weaponObjects = new List<GameObject>();

    public int slotLimit;
    public int weaponIndex;

    private void Awake()
    {
        carAttributes = GetComponent<CarAttributes>();
        slotLimit = carAttributes.gunPositions.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "UpdatePoint")
        {
            other.gameObject.SetActive(false);
            carUpdatePanel.SetActive(true);
            CoreGameSignals.Instance.onGamePause?.Invoke();
        }
        if(other.tag == "CollectibleWeapon")
        {
            other.gameObject.SetActive(false);
            weaponIndex = other.GetComponent<GunPoint>().weaponType;
            if (carAttributes.mountedGuns.Contains(weaponIndex))
            {
                gunUpdatePanel.SetActive(true);
                CoreGameSignals.Instance.onGamePause?.Invoke();
                gunAttributes = weaponObjects[weaponIndex].GetComponent<GunAttributes>();
            }
            else 
            {
                if(slotLimit == carAttributes.mountedGuns.Count)
                {
                    foreach (Button button in gunSlotPanel.GetComponentsInChildren<Button>())
                    {
                        button.interactable = true;
                    }
                }
                else
                {
                    carAttributes.mountedGuns.Add(weaponIndex);
                    int slotIndex = carAttributes.mountedGuns.IndexOf(weaponIndex);
                    GameObject gunObj = weaponObjects[weaponIndex];
                    gunObj.transform.parent = carAttributes.transform;
                    gunObj.transform.position = carAttributes.gunPositions[slotIndex].position;
                    gunObj.SetActive(true);
                    gunSlotPanel.transform.GetChild(slotIndex).GetComponentInChildren<TMP_Text>().text = "";//gunObj.GetComponent<GunAttributes>().weaponType;
                    gunSlotPanel.transform.GetChild(slotIndex).GetComponentInChildren<Image>().sprite = gunObj.GetComponent<GunAttributes>().image;
                }
            }
        }
    }

    public void SetCarAttributes(int attributeIndex)
    {
        if (attributeIndex == 0)
        {
            carAttributes.Speed++;
        }
        if (attributeIndex == 1)
        {
            carAttributes.HP++;
        }
        if (attributeIndex == 2)
        {
            carAttributes.Defense++;
        }
        carUpdatePanel.SetActive(false);
        CoreGameSignals.Instance.onGameResume?.Invoke();
    }
    public void SetGunAttributes(int attributeIndex)
    {
        if (attributeIndex == 0)
        {
            gunAttributes.GunDamage++;
        }
        if (attributeIndex == 1)
        {
            gunAttributes.GunShootingSpeed++;
        }
        if (attributeIndex == 2)
        {
            gunAttributes.GunChargerCapacity++;
        }
        gunUpdatePanel.SetActive(false);
        CoreGameSignals.Instance.onGameResume?.Invoke();
    }

    public void ChangeGun(int slotIndex)
    {
        weaponObjects[carAttributes.mountedGuns[slotIndex]].SetActive(false);
        carAttributes.mountedGuns[slotIndex] = weaponIndex;
        GameObject gunObj = weaponObjects[weaponIndex];
        gunObj.transform.parent = carAttributes.transform;
        gunObj.transform.position = carAttributes.gunPositions[slotIndex].position;
        gunObj.SetActive(true);
        gunSlotPanel.transform.GetChild(slotIndex).GetComponentInChildren<TMP_Text>().text = gunObj.GetComponent<GunAttributes>().weaponType;

        foreach (Button button in gunSlotPanel.GetComponentsInChildren<Button>())
        {
            button.interactable = false;
        }
    }

}
