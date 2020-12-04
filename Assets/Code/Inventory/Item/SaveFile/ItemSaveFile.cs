using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemSaveFile
{
    public ItemID ID = ItemID.Empty;
    public int stacks = 1;

    public ItemSaveFile() {}

    public ItemSaveFile(ItemSaveFile clone)
    {
        ID      = clone.ID;
        stacks  = clone.stacks;
    }

    public ItemSaveFile(ItemID ID, int stacks) 
    {
        this.ID     = ID;
        this.stacks = stacks;
    }

    public ItemSaveFile(Item item)
    {
        ID      = item.ID;
        stacks  = item.stacks;
    }

    public void Clear ()
    {
        ID = ItemID.Empty;
        stacks = 1;
    }
}