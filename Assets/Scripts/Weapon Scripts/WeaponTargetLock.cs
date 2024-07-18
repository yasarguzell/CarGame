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
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField][Tooltip("How much closer the new target cannidate neets to be for target change.")] float differenceThreshold = 1f;
    [SerializeField] public Vector3 trackOffset;
    [SerializeField] public float trackLimit;
    //

    private Vector3 startLocalDir;
    private GameObject[] targetParentArray;
    Transform targetsParent;

    private void Start()
    {
        targetParentArray = GameObject.FindGameObjectsWithTag("EnemyParent");

        startLocalDir = this.transform.InverseTransformDirection(this.transform.forward);
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
        Vector3 dir = this.transform.TransformDirection(startLocalDir);
        if (targetTransform)
        {
            dir = ((targetTransform.position + trackOffset) - componentTransform.position).normalized;
        }
        Quaternion rot = Quaternion.LookRotation(dir);
        rot = Quaternion.Lerp(componentTransform.rotation, rot, rotationSpeed * Time.deltaTime);
        componentTransform.rotation = rot;
       
        //Restrict track
        componentTransform.localRotation = Quaternion.Euler(Vector3.Scale(componentTransform.localRotation.eulerAngles, componentFreeAxis));
    }

    void FindClosestTrackTarget()
    {
        targetParentArray = GameObject.FindGameObjectsWithTag("EnemyParent");

        for (int k = 0; k < targetParentArray.Length; k++)
        {
            targetsParent = targetParentArray[k].transform;

            for (int i = 0; i < targetsParent.childCount; i++)
            {
                Debug.Log(targetsParent.childCount + " " + i);
                Transform tempChild = targetsParent.GetChild(i).GetChild(0);

                EnemyBase tempEnemyBase = tempChild.GetComponent<EnemyBase>();
                float testDistance = Vector3.Distance(this.transform.position, tempChild.position);
                bool isTempViable = tempEnemyBase & tempEnemyBase.GetCurrentHealth() > 0f & testDistance < trackLimit;

                if (targetTransform == null)
                {
                    targetTransform = tempChild;
                }

                EnemyBase targetEnemyBase = targetTransform.GetComponent<EnemyBase>();
                float currentDistance = Vector3.Distance(this.transform.position, targetTransform.position);
                bool isTargetViable = targetEnemyBase & targetEnemyBase.GetCurrentHealth() > 0f & currentDistance < trackLimit;

                if (isTargetViable)
                {
                    if (currentDistance > testDistance + differenceThreshold & isTempViable)
                    {
                        targetTransform = tempChild;
                    }
                }
                else if (isTempViable)
                {
                    targetTransform = tempChild;
                }
                else
                {
                    targetTransform = null;
                }
            }
        }
    }
}
