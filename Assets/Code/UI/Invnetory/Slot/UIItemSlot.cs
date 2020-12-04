using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [HideInInspector] public bool isInventorySlot;

    [SerializeField] Color highlightColor;

    [SerializeField] Image image;
    [SerializeField] Text countText;
    [SerializeField] GameObject descriptionGameObject;
    [SerializeField] Text descriptionText;
    [SerializeField] Text itemNameText;
    int slotIndex = -1;

    ItemSaveFile itemFile;
    ItemDirectory iconDir;
    SlotManager inventory;

    ItemSaveFile ClonedExistingItemFile => new ItemSaveFile(itemFile);


    public void Initialize(SlotManager inventory, int slotIndex)
    {
        this.inventory = inventory;
        this.slotIndex = slotIndex;

        try
        {
            ForceSetItemInSlot(inventory.ItemList[slotIndex]);
        }
        catch (Exception e)
        {
            Debug.LogError("Inventory name: " + inventory + "; Inventory.ItemList " + inventory.ItemList.Length + "; SlotIndex: " + slotIndex);
        }
    }

    //This name makes it explicit that we're overriding the slot and not asking for permission to swap.
    public void ForceSetItemInSlot(ItemSaveFile newItem)
    {
        if (newItem != null && newItem.ID != ItemID.Empty)
        {
            //Set icon
            image.sprite = ItemDirectory.GetItem(newItem.ID).icon;

            //Set text
            countText.text = newItem.stacks > 1 ? newItem.stacks.ToString() : string.Empty;

            //Set reference
            this.itemFile = newItem;
        }
    }

    bool IsItemFileEmpty(ItemSaveFile item) => item == null || item.ID == ItemID.Empty;
    public bool RequestDragDrop(ItemSaveFile newFile, out ItemSaveFile existingFile)
    {
        existingFile = null;

        if (IsItemFileEmpty(newFile))
        {
            return false;
        }

        if (inventory.TryAddToSpecificSlot(newFile, slotIndex, out existingFile))
        {
            //Clone the current ItemSaveFile for the output
            if (IsItemFileEmpty(existingFile))
            {
                existingFile = ClonedExistingItemFile;
            }
            itemFile = newFile;
            return true;
        }
        return false;
    }

    public void UseItem()
    {
        Item i = GetItem();
        if (i != null && i.TryUseItem())
        {
            inventory.ReduceStackableItemInInventory(slotIndex);
        }
    }

    public void ClearSlot()
    {
        //image.enabled = false;
        image.sprite = null;
        countText.text = string.Empty;
    }

    public void AddItem(ItemSaveFile newItem)
    {
        itemFile = newItem;
    }


    #region IPointer
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
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
            this.itemFile = itemFile;
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
    #endregion

    #region Helper methods
    Item GetItem()
    {
        ItemSaveFile itemSaveFile = inventory.ItemList[slotIndex];
        if (itemSaveFile != null)
        {
            ItemID id = itemSaveFile.ID;
            if (id != ItemID.Empty)
                return ItemDirectory.GetItem(itemSaveFile.ID);
        }
        return null;
    }
    #endregion
}