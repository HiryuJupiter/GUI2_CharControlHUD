using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace MyNameSpace
{
    public class Inventory_Consumable : Inventory
    {
        void Awake()
        {
            acceptingCondition = (item, slotIndex) => item.ItemType == ItemType.Consumable;
        }

        void Start()
        {
            //Load data from game data
            itemList = PersistentGameData.Instance.SaveFile.consumableBarItems;
            //Conditional
            UISlotManager_ConsumableBar.Instance.Initialize(this);
        }

        //protected override void ItemSlotted(int slotIndex) { }
        //protected override void ItemUnslotted(int slotIndex) { }
        //protected override void ItemSlotted(int slotIndex) => GetItemFromID(itemList[slotIndex].ID).ItemUnslotted();

    }
}