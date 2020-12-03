using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class UIItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [HideInInspector] public bool isInventorySlot;

    [SerializeField] Color highlightColor;

    [SerializeField] Image image;
    [SerializeField] Text countText;
    [SerializeField] GameObject descriptionGameObject;

    Item item;
    ItemIconsDirectory iconDir;
    Image emptyImage;

    public void UseItem ()
    {

    }

    public void UpdateSlotInfo(Item item)
    {
        //Set icon
        image.sprite = ItemIconsDirectory.GetSprite(item.itemType);

        //Set text
        countText.text = item.amount > 1 ? item.amount.ToString() : string.Empty;

        //Set reference
        this.item = item;

        ////Set text
        //descriptionText.text = a.Description;
        //nameText.text = a.name;
    }

    public void ClearSlot()
    {
        image.sprite = null;
        countText.text = string.Empty;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = highlightColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            Debug.Log("Left click");
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
            Debug.Log("Right click");
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
    }


    public void OnDrop(PointerEventData data)
    {
        if (data.pointerDrag != null)
        {
            this.item = item;
            Debug.Log("Dropped object was: " + data.pointerDrag);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}