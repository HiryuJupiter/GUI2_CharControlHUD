using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class UIBagInventory : UIInventory
{
    public static UIBagInventory Instance;

    #region MonoBehavior
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleOpen();
        }
    }
    #endregion

    #region Public - set sorting mode
    public void SortByWeapon()
    {
        sortOn = true;
        sortingMap = ItemSortingMaps.map_weaponFirst;
        RefreshInventoryDisplay();
        SortByNone();
    }

    public void SortByArmor()
    {
        sortOn = true;
        sortingMap = ItemSortingMaps.map_armorFirst;
        RefreshInventoryDisplay();
        SortByNone();
    }

    public void SortByConsumable()
    {
        sortOn = true;
        sortingMap = ItemSortingMaps.map_ConsumableFirst;
        RefreshInventoryDisplay();
        SortByNone();
    }

    public void SortByNone() => sortOn = false;
    #endregion
}

public static class ItemSortingMaps
{
    public static int[] map_weaponFirst = new int[]
    {
            (int) ItemType.Weapon,
            (int) ItemType.Armor,
            (int) ItemType.Consumable,
            (int) ItemType.Junk,
            (int) ItemType.QuestItem,
    };

    public static int[] map_armorFirst = new int[]
    {
            (int) ItemType.Armor,
            (int) ItemType.Weapon,
            (int) ItemType.Consumable,
            (int) ItemType.Junk,
            (int) ItemType.QuestItem,
    };

    public static int[] map_ConsumableFirst = new int[]
    {
            (int) ItemType.Consumable,
            (int) ItemType.Weapon,
            (int) ItemType.Armor,
            (int) ItemType.Junk,
            (int) ItemType.QuestItem,
    };
}