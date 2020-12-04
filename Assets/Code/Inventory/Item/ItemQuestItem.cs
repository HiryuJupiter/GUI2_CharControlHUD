using UnityEngine;
using System.Collections;

public class ItemQuestItem : Item
{
    public ItemQuestItem() 
    {
        isStackable = false;
        itemType = ItemType.QuestItem;
    }

}