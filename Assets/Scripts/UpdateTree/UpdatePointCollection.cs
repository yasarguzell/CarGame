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

    private void Awake()
    {
        carAttributes = GetComponent<CarAttributes>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "UpdatePoint")
        {
            other.gameObject.SetActive(false);
            carUpdatePanel.SetActive(true);
        }
        if(other.tag == "CollectibleWeapon")
        {
            other.gameObject.SetActive(false);
            int weaponIndex = other.GetComponent<GunPoint>().weaponType;
            if (carAttributes.mountedGuns.Contains(weaponIndex))
            {
                gunUpdatePanel.SetActive(true);
                gunAttributes = weaponObjects[weaponIndex].GetComponent<GunAttributes>();
            }
            else 
            {
                carAttributes.mountedGuns.Add(weaponIndex);
                int slotIndex = carAttributes.mountedGuns.IndexOf(weaponIndex);
                GameObject gunObj = weaponObjects[weaponIndex];
                gunObj.transform.parent = carAttributes.transform;
                gunObj.transform.position = carAttributes.gunPositions[slotIndex].position;
                gunObj.SetActive(true);
                gunSlotPanel.transform.GetChild(slotIndex).GetComponentInChildren<TMP_Text>().text = gunObj.GetComponent<GunAttributes>().weaponType;
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
    }

    //public void EquipGun(int weaponIndex)
    //{
    //    carAttributes.mountedGuns.Add(weaponIndex);
    //    weaponObjects[weaponIndex].transform.position = carAttributes.gunPositions[carAttributes.mountedGuns.IndexOf(weaponIndex)];
    //}

}
