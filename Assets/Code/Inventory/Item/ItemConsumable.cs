using UnityEngine;
using System.Collections;

public class ItemConsumable : Item
{
    public ItemConsumable() 
    {
        isStackable = true;
        itemType = ItemType.Consumable;
    }

    public override bool TryUseItem()
    {
        PlayerController.Instance.HealPlayer(20);
        return true;
    }
}