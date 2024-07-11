using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTargetLock : Weapon
{
    /*Component Properties*/
    [System.Serializable]
    public struct Component
    {
        public Transform weaponComponent;
        public Vector3 freeAxis;
    }
    /*Component Properties*/

    //
    [Header("Weapon Components")]
    [SerializeField] List<Component> componentList = new List<Component>();
    [Space]
    [Header("Track Properties")]
    [SerializeField] Transform targetsParent;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField][Tooltip("How much closer the new target cannidate neets to be for target change.")] float differenceThreshold = 1f;
    [SerializeField] public Vector3 trackOffset;
    //

    private void Start()
    {
        if (!targetsParent)
        {
            targetsParent = GameObject.Find("Enemies").transform;
        }
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
        Vector3 dir = ((targetTransform.position + trackOffset) - componentTransform.position).normalized;
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
            Transform tempChild = targetsParent.GetChild(i).GetChild(0);
            if (!targetTransform)
            {
                targetTransform = tempChild;
                FindClosestTrackTarget();
                break;
            }

            float currentDistance = Vector3.Distance(this.transform.position, targetTransform.position);
            float testDistance = Vector3.Distance(this.transform.position, tempChild.position);
            if (currentDistance > testDistance + differenceThreshold)
            {
                targetTransform = tempChild;
            }
        }
    }
}
