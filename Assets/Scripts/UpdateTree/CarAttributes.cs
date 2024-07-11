using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAttributes : MonoBehaviour
{
    public List<int> mountedGuns = new List<int>();
    public List<Transform> gunPositions = new List<Transform>();

    private float[] speeds = new float[] { 40f, 50f, 60f };
    public int speedIndex = 0;
    public float Speed  
    {
        get { return speeds[speedIndex]; }   
        set { if (speedIndex < speeds.Length - 1) { speedIndex++; } }  
    }

    private int[] hps = new int[] { 100, 200, 300 };
    public int hpIndex = 0;
    public int HP
    {
        get { return hps[hpIndex]; }
        set { if (hpIndex < hps.Length - 1) { hpIndex++; } }
    }

    private int[] defenses = new int[] { 50, 55, 60 };
    public int defenseIndex = 0;
    public int Defense
    {
        get { return defenses[defenseIndex]; }
        set { if (defenseIndex < defenses.Length - 1) { defenseIndex++; } }
    }


}
