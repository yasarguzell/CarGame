using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPoint : MonoBehaviour
{
    public int weaponType;
    public float cycleLength = 2f;

    private void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), cycleLength * 0.5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }
}
