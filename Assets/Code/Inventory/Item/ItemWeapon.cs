using UnityEngine;
using System.Collections;

public class ItemWeapon : Item
{
    public ItemWeapon() 
    {
        isStackable = false;
        itemType = ItemType.Weapon;
    }
}