using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Order of IPointer events:
//PointerDown, BeginDrag, Drag, PointUp, Click, OnEndDrag

public class test_DragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    GameObject clickedObject;

    RectTransform rectTransform;
    static bool dragging;

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
        RaycastResult result = eventData.pointerCurrentRaycast;
        if (result.gameObject != null)
        {
            test_DropReceiver script = result.gameObject.GetComponent<test_DropReceiver>();
            if (script != null)
            {
                Debug.Log("Can swap image");
            }
        }
        Debug.Log("[On end drag. PointerCurrentRaycast] = " + result);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("[OnPointerClick]");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clickedObject = eventData.lastPress;

        Debug.Log("[OnPointerDown]");
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        Debug.Log("[On Pointer Up]");
        if (!dragging  && gameObject == eventData.lastPress)
        {
            //If we didn't drag 
            Debug.Log(" I am clicked!");
        }
        clickedObject = null;
    }

    void OnGUI ()
    {
        GUI.Label(new Rect(20, 20, 200, 20), "clickedObject: " + clickedObject);
        GUI.Label(new Rect(20, 40, 200, 20), "dragging: " + dragging);
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
