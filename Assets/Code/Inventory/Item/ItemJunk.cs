using UnityEngine;
using System.Collections;


namespace MyNameSpace
{
    public class ItemJunk : Item
    {
        public ItemJunk()
        {
            isStackable = true;
            itemType = ItemType.Junk;
        }

    }
}