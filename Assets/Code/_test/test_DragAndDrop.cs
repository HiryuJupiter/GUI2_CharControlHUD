using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class test_DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    RectTransform rectTransform;

    void Awake ()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log(" on begin drag - spawn a ghost image");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log(" on  drag - follow mouse");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //eventData.lastPress is the gameobject you started the drag from, not the most recent objet you passed over, nor the object the mouseButtonUp is on.

        //eventData.button is the mouse button you are dragging with

        Debug.Log(", [SelectedObject] = " + eventData.selectedObject + "[PointerCurrentRaycast] = " + eventData.pointerCurrentRaycast);

        //if (event)
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(" on pointer down");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("A_Drop received");
    }
}

/*
     public LayerMask layerInteractable;

    void OnTriggerEnter(Collider col)
    {

        Debug.Log(" :" + col.gameObject.name);
        if (layerInteractable == (layerInteractable | 1 << col.gameObject.layer))
        {
            Debug.Log(" Hits the item: " + col.name);
        }
    }
 */
