using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

enum SortMode { Weapon, Armor, Consumable }

public class UIInventory : CanvasPageBase
{
    public static UIInventory Instance;

    [SerializeField] List<UIItemSlot> Slots = new List<UIItemSlot>();

    Inventory inventory;
    SortMode sortMode = SortMode.Weapon;
    int[] sortingMap;
    bool sortOn = false;

    void Awake()
    {
        Instance = this;
    }

    public void InitializeInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += RefershInventoryItems;
        inventory.TryAddItem(new Item { itemType = ItemType.Weapon, amount = 1 });
        inventory.TryAddItem(new Item { itemType = ItemType.Armor, amount = 1 });
        inventory.TryAddItem(new Item { itemType = ItemType.Consumable, amount = 1 });
        inventory.TryAddItem(new Item { itemType = ItemType.Weapon, amount = 1 });
        inventory.TryAddItem(new Item { itemType = ItemType.Weapon, amount = 1 });
        inventory.TryAddItem(new Item { itemType = ItemType.Weapon, amount = 1 });

        //RefershInventoryItems();
    }

    void RefershInventoryItems()
    {
        //Sort the items
        List<Item> items = sortOn ? inventory.GetItemList().OrderBy(x => sortingMap[(int)(x.itemType)]).ToList() : inventory.GetItemList();
        int count = items.Count;

        //Go through all slots and set them accordingly
        for (int i = 0; i < Slots.Count; i++)
        {
            if (i < count)
            {
                Slots[i].UpdateSlotInfo(items[i]);
            }
            else
            {
                Slots[i].ClearSlot();
            }
        }
    }

    public void SortByWeapon()
    {
        sortOn = true;
        sortingMap = ItemSortingMaps.map_weaponFirst;
        RefershInventoryItems();
    }

    public void SortByArmor()
    {
        sortOn = true;
        sortingMap = ItemSortingMaps.map_armorFirst;
        RefershInventoryItems();
    }

    public void SortByConsumable()
    {
        sortOn = true;
        sortingMap = ItemSortingMaps.map_ConsumableFirst;
        RefershInventoryItems();
    }

    public void SortByNone()
    {
        sortOn = false;
    }
}