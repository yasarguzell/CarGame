using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTargetLock : MonoBehaviour
{
    [System.Serializable]
    public struct Component
    {
        public Transform weaponComponent;
        public Vector3 freeAxis;
    }


    [Header("Weapon Components")]
    [SerializeField] List<Component> componentList = new List<Component>();
    [Space]
    [Header("Weapon Properties")]
    [SerializeField] Transform targetsParent;
    [SerializeField] float rotationSpeed = 1f;

    private Transform targetTransform;

    void Start()
    {
     
    }


    void Update()
    {
        FindClosestTrackTarget();
        TrackTarget();
    }

    void TrackTarget()
    {
        for (int i = 0; i < componentList.Count; i++)
        {
            ComponentTrackTargetRotation(componentList[i].weaponComponent, componentList[i].freeAxis, targetTransform);
        }
    }

    void ComponentTrackTargetRotation(Transform componentTransform, Vector3 componentFreeAxis, Transform targetTransform)
    {
        //Full interpolated track
        Vector3 dir = (targetTransform.position - componentTransform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);
        rot = Quaternion.Lerp(componentTransform.rotation, rot, rotationSpeed * Time.deltaTime);
        componentTransform.rotation = rot;
       
        //Restrict track
        componentTransform.localRotation = Quaternion.Euler(Vector3.Scale(componentTransform.localRotation.eulerAngles, componentFreeAxis));
    }

    void FindClosestTrackTarget()
    {
        for (int i = 0; i < targetsParent.childCount; i++)
        {
            Transform tempChild = targetsParent.GetChild(i);
            if (!targetTransform)
            {
                targetTransform = tempChild;
                FindClosestTrackTarget();
                break;
            }

            float currentDistance = Vector3.Distance(this.transform.position, targetTransform.position);
            float testDistance = Vector3.Distance(this.transform.position, tempChild.position);
            if (currentDistance > testDistance)
            {
                targetTransform = tempChild;
                Debug.Log(targetTransform.name);
            }
        }
    }
}