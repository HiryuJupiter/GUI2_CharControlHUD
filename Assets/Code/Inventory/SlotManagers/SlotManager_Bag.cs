using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SlotManager_Bag : SlotManager
{
    void Start()
    {
        //Load data from game data
        itemList = PersistentGameData.Instance.SaveFile.bagItems;
        //Conditional
        UIBagInventory.Instance.Initialize(this);

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
}