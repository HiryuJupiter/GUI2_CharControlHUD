using UnityEngine;
using System.Collections;

public class ItemConsumable : Item
{
    public ItemConsumable() 
    {
        isStackable = true;
        itemType = ItemType.Consumable;

        cooldownDuration = 0.5f;
    }

    public override bool TryUseItem()
    {
        if (CooldownReady)
        {
            CooldownTimer = cooldownDuration;
            SceneManager.Instance.StartCoroutine(StartCDTimer());


            PlayerController.Instance.HealPlayer(20);
            return true;
        }
        return false;
    }
}