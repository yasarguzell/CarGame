using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAttributes : MonoBehaviour
{
    public enum Attributes { Speed, HP, Defense, GunDamage, GunShootingSpeed, ChargerCapacity }

    private float[] speeds = new float[] { };
    public int speedIndex = 0;
    public float Speed  
    {
        get { return speeds[speedIndex]; }   
        set { speedIndex++; }  
    }

    private int[] hps = new int[] { };
    public int hpIndex = 0;
    public int Hp
    {
        get { return hps[hpIndex]; }
        set { hpIndex++; }
    }

    private int[] defenses = new int[] { };
    public int defenseIndex = 0;
    public int Defense
    {
        get { return defenses[defenseIndex]; }
        set { defenseIndex++; }
    }

    private int[] gunDamages = new int[] { };
    public int gunDamageIndex = 0;
    public int GunDamage
    {
        get { return gunDamages[gunDamageIndex]; }
        set { gunDamageIndex++; }
    }

    private float[] gunShootingSpeeds = new float[] { };
    public int gunShootingSpeedIndex = 0;
    public float GunShootingSpeed
    {
        get { return gunShootingSpeeds[gunShootingSpeedIndex]; }
        set { gunShootingSpeedIndex++; }
    }

    private int[] chargerCapacities = new int[] { };
    public int chargerCapacityIndex = 0;
    public int ChargerCapacity
    {
        get { return chargerCapacities[chargerCapacityIndex]; }
        set { chargerCapacityIndex++; }
    }

    private void Awake()
    {
        
    }
}
