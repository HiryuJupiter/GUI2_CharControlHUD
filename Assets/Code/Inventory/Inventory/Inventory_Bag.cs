using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace MyNameSpace
{
    public class Inventory_Bag : Inventory
    {
        public static Inventory_Bag Instance;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            //Load data from game data
            itemList = PersistentGameData.Instance.SaveFile.bagItems;
            //Conditional
            UISlotManager_Bag.Instance.Initialize(this);

            //DEBUG ONLY
            TryPickUpItem(new ItemSaveFile(ItemID.weapon, 1));
            TryPickUpItem(new ItemSaveFile(ItemID.armor, 2));
            TryPickUpItem(new ItemSaveFile(ItemID.potion, 3));
            TryPickUpItem(new ItemSaveFile(ItemID.potion, 3));
            TryPickUpItem(new ItemSaveFile(ItemID.weapon, 2));
            TryPickUpItem(new ItemSaveFile(ItemID.armor, 2));
            TryPickUpItem(new ItemSaveFile(ItemID.armor, 2));
            TryPickUpItem(new ItemSaveFile(ItemID.armor, 2));
            TryPickUpItem(new ItemSaveFile(ItemID.armor, 2));
            TryPickUpItem(new ItemSaveFile(ItemID.weapon, 2));
            TryPickUpItem(new ItemSaveFile(ItemID.weapon, 2));
        }

        //protected override void ItemSlotted(int slotIndex) { }
        //protected override void ItemUnslotted(int slotIndex) { }
        //protected override void ItemSlotted(int slotIndex) => GetItemFromID(itemList[slotIndex].ID).ItemUnslotted();

        public bool TryRemoveItems(ItemID[] itemsToRemove)
        {
            //First check if inventory contains those items
            foreach (ItemID removeID in itemsToRemove)
            {
                if (!InventoryContainsFileID(removeID))
                    return false;
            }

            //If it does, then remove them all
            foreach (ItemID removeID in itemsToRemove)
            {
                for (int i = 0; i < itemList.Length; i++)
                {
                    if (itemList[i] != null && itemList[i].ID == removeID)
                    {
                        ReduceStackableItemInInventory(i);
                        break;
                    }
                }
            }

            InvokeEvent_InventoryChange();
            return true;
        }

        bool InventoryContainsFileID(ItemID id)
        {
            foreach (var item in itemList)
            {
                if (item != null && item.ID == id)
                {
                    return true;
                }
            }
            return false;
        }

        void OnGUI()
        {
            if (GUI.Button(new Rect(20, 300, 100, 100), "Add weapon to backpack"))
            {
                TryPickUpItem(new ItemSaveFile(ItemID.weapon, 1));
            }
            if (GUI.Button(new Rect(220, 300, 100, 100), "Add armor to backpack"))
            {
                TryPickUpItem(new ItemSaveFile(ItemID.armor, 1));
            }
            if (GUI.Button(new Rect(420, 300, 100, 100), "Add potion to backpack"))
            {
                TryPickUpItem(new ItemSaveFile(ItemID.potion, 1));
            }
        }
    }
}