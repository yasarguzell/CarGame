
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShowroomCamera : MonoBehaviour
{

    public Transform target;        //  Camera target. Usually our spawn point.
    public float distance = 8f;     //  Z Distance.
    [Space]
    public bool orbitingNow = true;     //  Auto orbiting now?
    public float orbitSpeed = 5f;       //  Auto orbiting speed.
    [Space]
    public bool smooth = true;      //  Smooth orbiting.
    public float smoothingFactor = 5f;      //  Smooth orbiting factor.
    [Space]
    public float minY = 5f;     //  Minimum Y degree.
    public float maxY = 35f;        //  Maximum Y degree.
    [Space]
    public float dragSpeed = 10f;       //  Drag speed.
    public float orbitX = 0f;       //  Orbit X.
    public float orbitY = 0f;       //  Orbit Y.

    private void LateUpdate()
    {

        // If there is no target, return.
        if (!target)
        {

            Debug.LogWarning("Camera target not found!");
            enabled = false;
            return;

        }

        // If auto orbiting is enabled, increase orbitX slowly with orbitSpeed factor.
        if (orbitingNow)
            orbitX += Time.deltaTime * orbitSpeed;

        //  Clamping orbit Y.
        orbitY = ClampAngle(orbitY, minY, maxY);

        // Calculating rotation and position of the camera.
        Quaternion rotation = Quaternion.Euler(orbitY, orbitX, 0);
        Vector3 position = rotation * new Vector3(0f, 0f, -distance) + target.transform.position;

        // Setting position and rotation of the camera.
        if (!smooth)
        {

            transform.rotation = rotation;
            transform.position = position;

        }
        else
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.unscaledDeltaTime * 10f);
            transform.position = Vector3.Lerp(transform.position, position, Time.unscaledDeltaTime * 10f);

        }

    }

    private float ClampAngle(float angle, float min, float max)
    {

        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);

    }

    public void ToggleAutoRotation(bool state)
    {

        orbitingNow = state;

    }

    public void OnDrag(PointerEventData pointerData)
    {


        // Receiving drag input from UI.
        orbitX += pointerData.delta.x * dragSpeed * .02f;
        orbitY -= pointerData.delta.y * dragSpeed * .02f;

    }

}
