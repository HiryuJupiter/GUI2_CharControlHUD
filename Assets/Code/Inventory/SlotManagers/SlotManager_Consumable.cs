using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SlotManager_Consumable : SlotManager
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
        UIConsumableInventory.Instance.Initialize(this);
    }
}