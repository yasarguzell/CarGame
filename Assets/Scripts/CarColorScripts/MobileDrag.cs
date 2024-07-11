
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MobileDrag : MonoBehaviour, IDragHandler, IEndDragHandler
{

    private ShowroomCamera showroomCamera;

    private void Awake()
    {
        showroomCamera = FindObjectOfType<ShowroomCamera>();
    }

    public void OnDrag(PointerEventData data)
    {
        showroomCamera.OnDrag(data);
    }

    public void OnEndDrag(PointerEventData data)
    {
    }

}
