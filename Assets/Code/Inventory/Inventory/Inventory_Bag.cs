using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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
}