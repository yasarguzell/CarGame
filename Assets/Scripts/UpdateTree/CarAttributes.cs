using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAttributes : MonoBehaviour
{
    public enum Attributes { Speed, HP, Defense, GunDamage, GunShootingSpeed, ChargerCapacity }

    private float[] speeds = new float[] { 40f, 50f, 60f };
    public int speedIndex = 0;
    public float Speed  
    {
        get { return speeds[speedIndex]; }   
        set { if (speedIndex < speeds.Length - 1) { speedIndex++; } }  
    }

    private int[] hps = new int[] { 100 };
    public int hpIndex = 0;
    public int HP
    {
        get { return hps[hpIndex]; }
        set { if (hpIndex < hps.Length - 1) { hpIndex++; } }
    }

    private int[] defenses = new int[] { 50 };
    public int defenseIndex = 0;
    public int Defense
    {
        get { return defenses[defenseIndex]; }
        set { if (defenseIndex < defenses.Length - 1) { defenseIndex++; } }
    }

    private int[] gunDamages = new int[] { 1 };
    public int gunDamageIndex = 0;
    public int GunDamage
    {
        get { return gunDamages[gunDamageIndex]; }
        set { if (gunDamageIndex < gunDamages.Length - 1) { gunDamageIndex++; } }
    }

    private float[] gunShootingSpeeds = new float[] { 2f };
    public int gunShootingSpeedIndex = 0;
    public float GunShootingSpeed
    {
        get { return gunShootingSpeeds[gunShootingSpeedIndex]; }
        set { if (gunShootingSpeedIndex < gunShootingSpeeds.Length - 1) { gunShootingSpeedIndex++; } }
    }

    private int[] chargerCapacities = new int[] { 50 };
    public int chargerCapacityIndex = 0;
    public int ChargerCapacity
    {
        get { return chargerCapacities[chargerCapacityIndex]; }
        set { if (chargerCapacityIndex < chargerCapacities.Length - 1) { chargerCapacityIndex++; } }
    }

}
