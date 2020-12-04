﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SlotManager_Equipment : SlotManager
{
    public static SlotManager_Equipment Instance;

    const int WeaponL = 0;
    const int WeaponR = 1;
    const int Armor = 2;
    const int Legging = 3;

    void Awake()
    {
        Instance = this;
        condition = (item, slotIndex) => (
            (item.ItemType == ItemType.Weapon && (slotIndex == WeaponL || slotIndex == WeaponR)) ||
            (item.ItemType == ItemType.Armor && (slotIndex == Armor || slotIndex == Legging)));
    }

    void Start()
    {
        //Load data from game data
        itemList = PersistentGameData.Instance.SaveFile.equipmentItems;

        UIEquipmentInventory.Instance.Initialize(this);
    }
}


//public override bool TryAddItem(ItemSaveFile itemFile, int slotIndex)
//{
//    Item item = ItemFromID(itemFile.ID);

//    if ((item.ItemType == ItemType.Weapon && (slotIndex == WeaponL || slotIndex == WeaponR)) ||
//        (item.ItemType == ItemType.Armor && (slotIndex == Armor || slotIndex == Legging)))
//    {
//        //If the item is stackable, try stack it, otherwise try 
//        //add it directly.
//        if (item.IsStackable && TryStackItemInInventory(itemFile))
//        {
//            InvokeEvent_InventoryChange();
//            return true;
//        }

//        if (TryAddToSpecificSlotWithoutStacking(itemFile, slotIndex, out ItemSaveFile existing))
//        {
//            InvokeEvent_InventoryChange();
//            return true;
//        }
//    }
//    return false;
//}