using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SlotManager : MonoBehaviour
{
    public delegate void OnItemChangedHandler();
    public event OnItemChangedHandler OnItemListChanged;

    protected ItemSaveFile[] itemList;
    protected Item GetItemFromID (ItemID ID) => ItemDirectory.GetItem(ID);

    protected Func<Item, int, bool> condition;

    public ItemSaveFile[] ItemList => itemList;

    //void Start()
    //{
    //    //Load data from game data
    //    itemList = PersistentGameData.Instance.SaveFile.bagItems;
    //    //Conditional
    //}

    #region Public
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

    public bool TryAddToSpecificSlot(ItemSaveFile newItemFile, int slotIndex, out ItemSaveFile existing)
    {
        Item item = GetItemFromID(newItemFile.ID);
        existing = null;
        if (condition == null || condition(item, slotIndex))
        {
            //If there is no existing item, then just set it
            if (SlotIsEmpty(slotIndex))
            {
                SetItemFileAt(newItemFile, slotIndex);

                InvokeEvent_InventoryChange();
                return true;
            }
            else //If there is an existing item...
            {
                //If it is stackable, then stack it, if not, clone the reference type for the out parameter
                if (item.IsStackable && ItemsAreTheSame (itemList[slotIndex], item))
                {
                    itemList[slotIndex].stacks += newItemFile.stacks;

                    InvokeEvent_InventoryChange();
                    return true;
                }
                else
                {
                    existing = GetClonedItemFile(slotIndex);
                    SetItemFileAt(newItemFile, slotIndex);

                    InvokeEvent_InventoryChange();
                    return true;
                }
            }
        }
        return false;
    }

    

    //bool ItemListContains(ItemSaveFile newItem) => Array.Exists(itemList, e => e == newItem);
    #endregion

    #region Private
    bool TryAddToNextEmptySlot(ItemSaveFile newItem)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (SlotIsEmpty(i))
            {
                itemList[i] = newItem;
                InvokeEvent_InventoryChange();
                return true;
            }
        }
        return false;
    }

    bool TryStackItemInAnyslot (ItemSaveFile newItem)
    {
        //See if the item already exists in the inventory
        foreach (var i in itemList)
        {
            if (i.ID == newItem.ID)
            {
                i.stacks += newItem.stacks;
                InvokeEvent_InventoryChange();
                return true;
            }
        }
        return false;
    }

    public void ReduceStackableItemInInventory(int slot)
    {
        //If there is an item and it has stacks, then reduce it, otherwise delete
        if (!SlotIsEmpty(slot))
        {
            Item i = GetItemFromID(itemList[slot].ID);

            if (i.IsStackable && itemList[slot].stacks > 2)
            {
                itemList[slot].stacks --;
            }
        }
        else
        {
            ItemList[slot] = null;
        }

        InvokeEvent_InventoryChange();
    }
    #endregion

    protected void InvokeEvent_InventoryChange() => OnItemListChanged?.Invoke();
    protected bool SlotIsEmpty(int slot) => itemList[slot] == null || itemList[slot].ID == ItemID.Empty;
    protected ItemSaveFile ItemFileAt(int slot) => itemList[slot];
    protected void SetItemFileAt(ItemSaveFile itemFile, int slot) => itemList[slot] = itemFile;
    protected ItemSaveFile GetClonedItemFile (int slot) => new ItemSaveFile(itemList[slot]);
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
 */