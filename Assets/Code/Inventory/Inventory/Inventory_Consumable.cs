using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory_Consumable : Inventory
{
    void Awake()
    {
        condition = (item, slotIndex) => item.ItemType == ItemType.Consumable;
    }

    void Start()
    {
        //Load data from game data
        itemList = PersistentGameData.Instance.SaveFile.consumableBarItems;
        //Conditional
        UISlotManager_ConsumableBar.Instance.Initialize(this);
    }
}