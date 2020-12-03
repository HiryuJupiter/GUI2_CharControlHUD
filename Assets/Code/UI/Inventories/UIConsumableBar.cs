using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class UIConsumableBar : CanvasPageBase
{
    public static UIConsumableBar Instance;

    [SerializeField] List<UIItemSlot> Slots = new List<UIItemSlot>();

    Inventory inventory;

    void Awake()
    {
        Instance = this;
    }

    public void InitializeInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += RefershInventoryItems;
        inventory.TryAddItem(new Item { itemType = ItemType.Consumable, amount = 1 });

        //RefershInventoryItems();
    }

    void RefershInventoryItems()
    {
        //Sort the items
        List<Item> items = inventory.GetItemList();
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
}