using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory
{
    int slotCount = 8;

    public delegate void OnItemChangedHandler();
    public event OnItemChangedHandler OnItemListChanged;

    List<Item> itemList = new List<Item>();

    Predicate<Item> conditional;

    public Inventory(int slotCount, Predicate<Item> conditional)
    {
        this.slotCount = slotCount;
        this.conditional = conditional;
    }

    public bool TryAddItem(Item newItem)
    {
        //If the item is stackable, try stack it, otherwise try 
        //add it directly.

        if (conditional == null || conditional(newItem))
        {
            if (newItem.isStackable && TryStackItemInInventory(newItem))
            {
                OnItemListChanged?.Invoke();
                return true;
            }

            if (TryAddItemWithoutStacking(newItem))
            {
                OnItemListChanged?.Invoke();
                return true;
            }
        }
        
        return false;
    }

    bool TryStackItemInInventory(Item newItem)
    {
        //See if the item already exists in the inventory
        foreach (var i in itemList)
        {
            if (i.itemType == newItem.itemType)
            {
                i.amount += newItem.amount;
                OnItemListChanged?.Invoke();
                return true;
            }
        }
        return false;
    }

    bool TryAddItemWithoutStacking(Item newItem)
    {
        if (itemList.Count < slotCount)
        {
            itemList.Add(newItem);
            OnItemListChanged?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    bool TryRemoveStackableItemInInventory(Item newItem)
    {
        //See if the item already exists in the inventory
        foreach (var i in itemList)
        {
            if (i.itemType == newItem.itemType)
            {
                if (i.amount > 2)
                {
                    i.amount -= newItem.amount;
                    OnItemListChanged?.Invoke();
                    return true;
                }
                return false;
            }
        }
        return false;
    }

    bool TryRemoveItem (Item newItem)
    {
        if (itemList.Contains(newItem))
        {
            itemList.Remove(newItem);
            OnItemListChanged?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<Item> GetItemList() => itemList;
}