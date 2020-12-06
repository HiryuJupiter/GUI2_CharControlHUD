using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEditor;

//The UISlotManager and its UIItemSlot components MUST BE a perfect representation of what's inside the Inventory classes, so that they can delegate all management logic to Inventory and only deal with displaying info.

public class UIItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool IsVisibleOnScreen; 

    [HideInInspector] public bool isInventorySlot;

    [SerializeField] Color highlightColor;

    [SerializeField] Image image;
    [SerializeField] Image cooldownMask;
    [SerializeField] Text countText;
    [SerializeField] GameObject descriptionGameObject;
    [SerializeField] Text descriptionText;
    [SerializeField] Text itemNameText;
    int slotIndex = -1;

    ItemSaveFile itemFile;
    ItemDirectory iconDir;
    Inventory inventory;
    DragAndDrop dragAndDrop;

    //Status
    Item currentItem;
    bool hasValidItem;
    bool itemHasCooldown;

    ItemSaveFile ClonedExistingItemFile => new ItemSaveFile(itemFile);
    bool HasFile => itemFile != null && itemFile.ID != ItemID.Empty;
    public ItemSaveFile ItemFile => itemFile;

    #region MonoBehavior
    void Update ()
    {
        if (IsVisibleOnScreen && hasValidItem && itemHasCooldown)
        {
            cooldownMask.fillAmount = currentItem.CooldownPercent;
        }
    }
    #endregion

    #region Public - item management - control from Inventory
    public void Initialize(Inventory inventory, int slotIndex)
    {
        //References
        this.inventory = inventory;
        this.slotIndex = slotIndex;
        dragAndDrop = DragAndDrop.Instance;

        //Update the slot visuals
        try
        {
            SetSlotWithoutNotifyingInventory(inventory.ItemList[slotIndex]);
        }
        catch (Exception e)
        {
            Debug.Log("Inventory name: " + inventory);
            Debug.Log("; Inventory.ItemList " + inventory.ItemList);
            Debug.Log("; Inventory.ItemList " + inventory.ItemList.Length);
            Debug.Log("; SlotIndex: " + slotIndex);
        }
    }

    public void SetSlotWithoutNotifyingInventory(ItemSaveFile newItem)
    {
        if (newItem != null && newItem.ID != ItemID.Empty)
        {
            //Status
            currentItem = ItemDirectory.GetItem(newItem.ID);
            itemHasCooldown = currentItem.HasCooldown;
            hasValidItem = true;

            //Description
            descriptionText.text = currentItem.description;
            itemNameText.text = currentItem.itemName;

            //Set icon
            image.sprite = currentItem.icon;
            countText.text = (currentItem.IsStackable && newItem.stacks > 1) ? newItem.stacks.ToString() : string.Empty;

            //Set reference
            this.itemFile = newItem;


        }
        else
        {
            ClearSlotWithoutNotifyingInventory();
        }
    }

    public void ClearSlotWithoutNotifyingInventory()
    {
        //image.enabled = false;
        image.sprite = null;
        countText.text = string.Empty;
        itemFile = null;

        //Status
        currentItem = null;
        hasValidItem = false;
        itemHasCooldown = false;

        //Description
        descriptionText.text = String.Empty;
        itemNameText.text = String.Empty;

        cooldownMask.fillAmount = 0f;
    }

    public void UseItem()
    {
        if (TryGetItem(out Item item))
        {
            if (item != null && item.CooldownReady && item.TryUseItem())
            {
                inventory.ReduceStackableItemInInventory(slotIndex);
            }
        }
    }

    #endregion

    #region Check for timer update
    public void CheckForCooldownMask ()
    {

    }

    public void ClearCooldownMask ()
    {

    }
    #endregion

    #region Public - drag and drop - Requires notification
    //"Notify inventory" is just saying, we're not just changing the ui image and text, we want the save data to change
    public bool DragAndDrop_TryStackingItems_AndNotifyInventory(ItemSaveFile newFile)
    {
        return inventory.SlotRequest_TryStackItem(newFile, slotIndex);
    }

    public void DragAndDrop_ForceAssignFile_AndNotifyInventory(ItemSaveFile newFile)
    {
        inventory.SlotRequest_ForceAssignItem_NonSwapping(newFile, slotIndex);
    }

    public void DragAndDrop_ClearSlotAndNotifyInventory()
    {
        inventory.SlotRequest_ClearSlot(slotIndex);
    }

    public void DragAndDraop_DropItemOnGroundAndNotifyInventory()
    {
        if (HasFile)
        {
            ItemDirectory.SpawnItemAtPlayerPosition(itemFile);
            inventory.SlotRequest_ClearSlot(slotIndex);
        }
    }
    #endregion

    #region IPointer - basic interaction
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = highlightColor;

        if (hasValidItem && !DragAndDrop.IsDragging)
        {
            descriptionGameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
        descriptionGameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            UseItem();
        }
    }
    #endregion

    #region IPointer - drag and drop
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragAndDrop.StartDrag(this, itemFile);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragAndDrop.StopDrag(eventData.pointerCurrentRaycast.gameObject);
    }

    public void OnDrag(PointerEventData eventData) {}
    #endregion

    #region Private

    bool TryGetItem(out Item item)
    {
        item = null;
        ItemSaveFile itemSaveFile = inventory.ItemList[slotIndex];

        if (itemSaveFile != null)
        {
            ItemID id = itemSaveFile.ID;
            if (id != ItemID.Empty)
            {
                item = ItemDirectory.GetItem(itemSaveFile.ID);
                return true;
            }
        }
        return false;
    }
    #endregion

    //Helper expression bodies
    public bool CanAcceptFile(ItemSaveFile file) => inventory.SlotRequest_CheckIfCanAcceptFile(file, slotIndex);
    public bool CanReleaseFile(ItemSaveFile file) => inventory.SlotRequest_CheckIfCanReleaseFile(file, slotIndex);

    bool IsItemFileEmpty(ItemSaveFile item) => item == null || item.ID == ItemID.Empty;

}