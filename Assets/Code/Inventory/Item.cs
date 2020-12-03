using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="Item", menuName = "Inventory/Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public int amount;
    public bool isStackable;

    public Item() {}

    public Item(Item clone)
    {
        //This is for when we don't want references to mix
        this.itemType       = clone.itemType;
        this.amount         = clone.amount;
        this.isStackable    = clone.isStackable;
    }

    public virtual void Use ()
    {

    }
}