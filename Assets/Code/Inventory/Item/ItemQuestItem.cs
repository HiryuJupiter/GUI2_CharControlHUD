using UnityEngine;
using System.Collections;


namespace MyNameSpace
{
    public class ItemQuestItem : Item
    {
        public ItemQuestItem()
        {
            isStackable = false;
            itemType = ItemType.QuestItem;
        }

    }
}