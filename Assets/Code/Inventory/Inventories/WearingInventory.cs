using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum EquipmentParts { WeaponLeft, WeaponRight, Armor, Pants}
public class WearingInventory
{
    public delegate void OnItemChangedHandler();
    public event OnItemChangedHandler OnItemListChanged;

    Item weaponLeft;
    Item weaponRight;
    Item armor;
    Item pants;

    public Item WeaponLeft => weaponLeft;
    public Item WeaponRight => weaponRight;
    public Item Armor => armor;
    public Item Pants => pants;

    public bool TryAddItem(Item newItem, EquipmentParts part, out Item existingItem)
    {
        existingItem = null;

        if (newItem.itemType == ItemType.Armor || newItem.itemType == ItemType.Weapon)
        {
            Item current = GetBodyPartItem(part);

            if (current != null)
            {
                //Clone the item so we are not pointing to the same reference
                existingItem = new Item(existingItem);
            }
            AddItem(newItem, part);
            return true;
        }

        return false;
    }

    void AddItem(Item newItem, EquipmentParts part)
    {
        switch (part)
        {
            case EquipmentParts.WeaponLeft:
                weaponLeft  = newItem;
                break;
            case EquipmentParts.WeaponRight:
                weaponRight = newItem;
                break;
            case EquipmentParts.Armor:
                armor       = newItem;
                break;
            case EquipmentParts.Pants:
            default:
                pants       = newItem;
                break;
        }
    }

    Item GetBodyPartItem (EquipmentParts part)
    {
        switch (part)
        {
            case EquipmentParts.WeaponLeft:
                return weaponLeft;
            case EquipmentParts.WeaponRight:
                return weaponRight;
            case EquipmentParts.Armor:
                return armor;
            case EquipmentParts.Pants:
            default:
                return pants;
        }
    }
}