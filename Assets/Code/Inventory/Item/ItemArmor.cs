using UnityEngine;
using System.Collections;

public class ItemArmor : Item
{
    public ItemArmor() 
    {
        isStackable = false;
        itemType = ItemType.Armor;
    }

}