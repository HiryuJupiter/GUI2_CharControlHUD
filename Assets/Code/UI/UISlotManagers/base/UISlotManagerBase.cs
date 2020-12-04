using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class UISlotManagerBase
    : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected List<UIItemSlot> Slots = new List<UIItemSlot>();
    protected Inventory inventory;

    protected int[] sortingMap;
    protected bool sortOn = false;
    protected bool isOpen;

    public int SlotCount => Slots.Count;

    public virtual void Initialize(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += RefreshInventoryDisplay;
        inventory.OnSlotChanged += RefreshSlotDisplay;

        for (int i = 0; i < Slots.Count; i++)
        {
            Slots[i].Initialize(inventory, i);
        }

        //inventory.TryAddItem(new ItemSaveFile(0, 1));
        RefreshInventoryDisplay();
    }

    protected virtual void RefreshSlotDisplay (int slotIndex)
    {
        Slots[slotIndex].SetSlotWithoutNotifyingInventory(inventory.ItemList[slotIndex]);
    }

    protected virtual void RefreshInventoryDisplay()
    {
        if (!isOpen) return;

        //Sort the items
        if (sortOn)
        {
            inventory.ReorderingInventory(sortingMap);
        }

        //Go through all slots and set them accordingly
        for (int i = 0; i < Slots.Count; i++)
        {
            Slots[i].SetSlotWithoutNotifyingInventory(inventory.ItemList[i]);
            ////Set the slots within the item range to display items, and the ones outside the range to be empty.
            //if (i < inventory.ItemList.Length)
            //{
            //    Slots[i].SetSlotWithoutNotifyingInventory(inventory.ItemList[i]);
            //}
            //else
            //{
            //    Slots[i].ClearSlotWithoutNotifyingInventory();
            //}
        }
    }

    public void ToggleOpen()
    {
        SetIsOpen(isOpen = !isOpen);
    }

    public virtual void SetIsOpen(bool isOpen)
    {
        this.isOpen = isOpen;
        if (isOpen)
        {
            CanvasGroupUtil.InstantReveal(canvasGroup);
            RefreshInventoryDisplay();
        }
        else
        {
            CanvasGroupUtil.InstantHide(canvasGroup);
        }

        foreach (var slot in Slots)
        {
            slot.IsVisibleOnScreen = isOpen;
        }
    }

    void OnDestroy()
    {
        inventory.OnItemListChanged -= RefreshInventoryDisplay;
        inventory.OnSlotChanged -= RefreshSlotDisplay;
    }
}

/*
    protected virtual void RefreshInventoryDisplay()
    {
        if (!isOpen) return;

        //Sort the items
        //List<Item> items = sortOn ? inventory.ItemList.OrderBy(x => sortingMap[(int)(x.ItemType)]).ToList() : inventory.ItemList;

        //Sort the items
        if (sortOn)
        {
            inventory.ReorderingInventory(sortingMap);

            //Select the items where the ID is not empty, and then get the item from the item directory
            var i = inventory.ItemList.Where(x => x.ID != ItemID.Empty).Select(x => ItemDirectory.GetItem(x.ID));
            items = i.OrderBy(x => sortingMap[(int)(x.ItemType)]).ToList();
        }
        else
        {
            items = inventory.ItemList.Where(x => x.ID != ItemID.Empty).Select(x => ItemDirectory.GetItem(x.ID)).ToList();
        }

        //Go through all slots and set them accordingly
        for (int i = 0; i < Slots.Count; i++)
        {
            //Set the slots within the item range to display items, and the ones outside the range to be empty.
            if (i < items.Count)
            {
                Slots[i].ForceSetItemInSlot(new ItemSaveFile(items[i]));
            }
            else
            {
                Slots[i].ClearSlot();
            }
        }
    }
 */

