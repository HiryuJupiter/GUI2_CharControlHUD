using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChangedHandler();
    public event OnItemChangedHandler OnItemListChanged;
    public delegate void OnSlotChangedHandler(int slotIndex);
    public event OnSlotChangedHandler OnSlotChanged;

    protected ItemSaveFile[] itemList;
    protected Item GetItemFromID(ItemID ID) => ItemDirectory.GetItem(ID);

    protected Func<Item, int, bool> acceptingCondition;
    protected Func<Item, int, bool> releasingCondition;

    public ItemSaveFile[] ItemList => itemList;

    //void Start()
    //{
    //    //Load data from game data
    //    itemList = PersistentGameData.Instance.SaveFile.bagItems;
    //    //Conditional
    //}

    #region Pick up item
    public virtual bool TryPickUpItem(Item newItem) => TryPickUpItem(new ItemSaveFile(newItem.ID, newItem.stacks));
    public virtual bool TryPickUpItem(ItemSaveFile newItemFile)
    {
        Item item = GetItemFromID(newItemFile.ID);

        //If the item is stackable, try stack it
        if (item.IsStackable && TryStackItemInAnyslot(newItemFile))
        {

            InvokeEvent_InventoryChange();
            return true;
        }

        //Add to next empty slot
        if (TryAddToNextEmptySlot(newItemFile))
        {
            InvokeEvent_InventoryChange();
            return true;
        }
        return false;

    }

    bool TryAddToNextEmptySlot(ItemSaveFile newItem)
    {

        Debug.Log(" a");
        for (int i = 0; i < itemList.Length; i++)
        {
            if (SlotIsEmpty(i))
            {
                itemList[i] = new ItemSaveFile(newItem);

                return true;
            }
        }
        Debug.Log(" b");

        return false;
    }

    bool TryStackItemInAnyslot(ItemSaveFile newItem)
    {
        //See if the item already exists in the inventory
        foreach (var i in itemList)
        {
            if (i != null && i.ID == newItem.ID)
            {
                i.stacks += newItem.stacks;
                return true;
            }
        }
        return false;
    }
    #endregion

    #region Inputs from slots (Drag and drop event)
    //shop only
    public bool SlotRequest_CheckIfCanReleaseFile(ItemSaveFile file, int slotIndex)
    {
        if (file != null && file.ID != ItemID.Empty)
        {
            Item item = GetItemFromID(file.ID);
            return (releasingCondition == null || releasingCondition(item, slotIndex)) ? true : false;
        }
        return false;
    }

    public bool SlotRequest_CheckIfCanAcceptFile(ItemSaveFile file, int slotIndex)
    {
        if (file != null && file.ID != ItemID.Empty)
        {
            Item item = GetItemFromID(file.ID);
            return (acceptingCondition == null || acceptingCondition(item, slotIndex)) ? true : false;
        }
        return false;
    }


    public bool SlotRequest_TryStackItem(ItemSaveFile newFile, int slotIndex)
    {
        if (itemList[slotIndex].ID == newFile.ID && ItemDirectory.GetItem(newFile.ID).IsStackable)
        {
            itemList[slotIndex].stacks += newFile.stacks;
            InvokeEvent_SlotChange(slotIndex);
            return true;
        }
        return false;
    }

    public void SlotRequest_ForceAssignItem_NonSwapping(ItemSaveFile newFile, int slotIndex)
    {
        //A brute force assignment of item, not asing to swap out any file. This method is used when preverifications have all being made. It's not a perfect solution but it works for now.
        Item item = GetItemFromID(newFile.ID);

        //Just put it in if this slot is empty
        if (SlotIsEmpty(slotIndex))
        {
            SetItemFileAt(newFile, slotIndex);
            OnItemSlotted(slotIndex);
            InvokeEvent_SlotChange(slotIndex);
        }
        else //If not empty, try stacking
        {
            if (item.IsStackable && ItemsAreTheSame(itemList[slotIndex], item))
            {
                itemList[slotIndex].stacks += newFile.stacks;
                InvokeEvent_SlotChange(slotIndex);
            }
            else //If not stackable then OVERRIDE IT
            {
                SetItemFileAt(newFile, slotIndex);
                OnItemSlotted(slotIndex);
                InvokeEvent_SlotChange(slotIndex);
            }
        }
    }

    public void SlotRequest_ClearSlot(int slotIndex)
    {
        OnItemUnslotted(slotIndex);

        itemList[slotIndex] = null;
        InvokeEvent_SlotChange(slotIndex);
    }
    #endregion

    #region General inventory management
    public void ReduceStackableItemInInventory(int slot)
    {
        //If there is an item and it has stacks, then reduce it, otherwise delete
        if (!SlotIsEmpty(slot))
        {
            Item i = GetItemFromID(itemList[slot].ID);

            if (i.IsStackable && itemList[slot].stacks > 2)
            {

                Debug.Log(" ReduceStackableItemInInventory] - " + itemList[slot].stacks);
                itemList[slot].stacks--;
            }
            else
            {
                ItemList[slot] = null;
            }
            InvokeEvent_InventoryChange();
        }
        else
        {
            Debug.LogError(" You are trying to reduce a stackabe item in an itemslot without any items!");
        }
    }

    public void ReorderingInventory(int[] sortingMap)
    {
        //Select the items where the ID is not empty, and then get the item from the item directory

        //Create an ordering tabel to associate the itemSaveFiles with an integer representing their itemType (enum).
        List<(ItemSaveFile, int)> orderingTable = new List<(ItemSaveFile, int)>();
        foreach (ItemSaveFile item in itemList)
        {
            //Fill the table with valid entries
            if (item != null && item.ID != ItemID.Empty)
            {
                orderingTable.Add((item, ItemDirectory.GetItemTypeInt(item.ID)));
            }
        }

        //Order the table using an integer array sorting map
        orderingTable = orderingTable.OrderBy(x => sortingMap[(x.Item2)]).ToList();

        //Update the itemList with these values
        for (int i = 0; i < itemList.Length; i++)
        {
            if (i < orderingTable.Count)
            {
                itemList[i] = new ItemSaveFile(orderingTable[i].Item1.ID, orderingTable[i].Item1.stacks);
            }
            else
            {
                ClearSlotWithoutNotify(i);
            }
        }
    }

    public void ClearSlotWithoutNotify(int slot) => itemList[slot] = new ItemSaveFile(ItemID.Empty, 1);
    #endregion

    protected virtual void OnItemSlotted(int slotIndex) { }
    protected virtual void OnItemUnslotted(int slotIndex) { }
    protected void InvokeEvent_InventoryChange() => OnItemListChanged?.Invoke();
    protected void InvokeEvent_SlotChange(int slotIndex) => OnSlotChanged?.Invoke(slotIndex);
    protected bool SlotIsEmpty(int slot) => itemList[slot] == null || itemList[slot].ID == ItemID.Empty;
    protected ItemSaveFile ItemFileAt(int slot) => itemList[slot];
    protected void SetItemFileAt(ItemSaveFile itemFile, int slot) => itemList[slot] = itemFile;
    protected ItemSaveFile GetClonedItemFile(int slot) => new ItemSaveFile(itemList[slot]);
    protected bool ItemsAreTheSame(ItemSaveFile itemFile, Item item) => item.ID == itemFile.ID;
}

/*
 public bool TryDeleteItem(ItemSaveFile itemFile)
    {
        foreach (var i in itemList)
        {
            if (i.ID == itemFile.ID)
            {
                if (ItemFromID(itemFile.ID).IsStackable && TryReduceStackableItemInInventory(itemFile)) {}
                else
                {
                    i.Clear();
                }
                InvokeEvent_InventoryChange();
                return true;
            }
        }
        return false;
    }
 
public bool TryReduceStackableItemInInventory(ItemSaveFile newItem)
    {
        //See if the item already exists in the inventory
        foreach (var i in itemList)
        {
            if (i.ID == newItem.ID)
            {
                if (i.stacks > 2)
                {
                    i.stacks -= newItem.stacks;
                    InvokeEvent_InventoryChange();
                    return true;
                }
                else
                {

                }
                return false;
            }
        }
        return false;

        if (slot < ItemList.Length)
        {
            ItemList[slot] = null;
        }
    }


    public void ReorderingInventory (int[] sortingMap)
    {
        //Select the items where the ID is not empty, and then get the item from the item directory

        //Create an ordering tabel to associate the itemSaveFiles with an integer representing their itemType (enum).
        List<(ItemSaveFile, int)> orderingTable = new List<(ItemSaveFile, int)>();
        foreach (var item in itemList)
        {
            if (item != null && item.ID != ItemID.Empty)
            {
                orderingTable.Add((item, ItemDirectory.GetItemTypeInt(item.ID)));
            }
        }
        orderingTable = orderingTable.OrderBy(x => sortingMap[(x.Item2)]).ToList();

        //Now order the table
        var nonEmpty = itemList.Where(x => x != null && x.ID != ItemID.Empty).Select(x => ItemDirectory.GetItem(x.ID));
        var ordered = nonEmpty.OrderBy(x => sortingMap[(int)(x.ItemType)]).ToList();
        itemList = ordered.Select(x => new ItemSaveFile(x.ID, x.;

        jkhjgkhhjk
        //var i = inventory.ItemList.Where(x => x.ID != ItemID.Empty).Select(x => ItemDirectory.GetItem(x.ID));
        //items = i.OrderBy(x => sortingMap[(int)(x.ItemType)]).ToList();
    }
 */