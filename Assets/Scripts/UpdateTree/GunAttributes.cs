using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GunAttributes : MonoBehaviour
{
    public string weaponType;

    private int[] gunDamages = new int[] { 1, 2, 3 };
    public int gunDamageIndex = 0;
    public int GunDamage
    {
        get { return gunDamages[gunDamageIndex]; }
        set { if (gunDamageIndex < gunDamages.Length - 1) { gunDamageIndex++; } }
    }

    private float[] gunShootingSpeeds = new float[] { 2f, 2.5f, 4f };
    public int gunShootingSpeedIndex = 0;
    public float GunShootingSpeed
    {
        get { return gunShootingSpeeds[gunShootingSpeedIndex]; }
        set { if (gunShootingSpeedIndex < gunShootingSpeeds.Length - 1) { gunShootingSpeedIndex++; } }
    }

    private int[] gunChargerCapacities = new int[] { 50, 60, 70 };
    public int gunChargerCapacityIndex = 0;
    public int GunChargerCapacity
    {
        get { return gunChargerCapacities[gunChargerCapacityIndex]; }
        set { if (gunChargerCapacityIndex < gunChargerCapacities.Length - 1) { gunChargerCapacityIndex++; } }
    }

}
