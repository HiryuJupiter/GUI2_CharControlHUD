using UnityEngine;
using System.Collections;

public class ItemJunk : Item
{
    public ItemJunk() 
    {
        isStackable = true;
        itemType = ItemType.Junk;
    }

}