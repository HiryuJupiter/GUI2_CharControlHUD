using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class UIEquipmentInventory : CanvasPageBase
{
    public static UIEquipmentInventory Instance;

    [SerializeField]  UIItemSlot weaponLeft;
    [SerializeField]  UIItemSlot weaponRight;
    [SerializeField]  UIItemSlot armor;
    [SerializeField]  UIItemSlot pants;

    WearingInventory inventory;

    void Awake()
    {
        Instance = this;
    }

    public void InitializeInventory(WearingInventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += RefershInventoryItems;
        inventory.TryAddItem(new Item { itemType = ItemType.Armor, amount = 1 }, EquipmentParts.WeaponLeft, out Item existing);

        //RefershInventoryItems();
    }

    void RefershInventoryItems()
    {
        //Go through all slots and set them accordingly
        UpdateSlot(inventory.WeaponLeft, weaponLeft);
        UpdateSlot(inventory.WeaponRight, weaponRight);
        UpdateSlot(inventory.Armor, armor);
        UpdateSlot(inventory.Pants, pants);
    }

    void UpdateSlot (Item item, UIItemSlot uiSlot)
    {
        if (item == null) //If there is no item, then clear the slot
        {
            uiSlot.ClearSlot();
        }
        else
        {
            uiSlot.UpdateSlotInfo(item);
        }
    }
}