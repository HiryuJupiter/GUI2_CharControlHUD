using UnityEngine;
using System.Collections;

//[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public abstract class  Item : MonoBehaviour
{
    public ItemID ID;
    public string itemName;
    public string description;

    public int price = 1;
    public Sprite icon;

    [HideInInspector] public int stacks = 1;

    protected bool isStackable;
    protected ItemType itemType;

    protected SpriteRenderer spriteRenderer;
    protected TextMesh textMesh;

    //Property
    public bool IsStackable => isStackable;
    public ItemType ItemType => itemType;

    public virtual bool TryUseItem() => false; //Items cannot be used by default

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public GameObject SpawnItem(Vector3 position) => Instantiate(gameObject, position, Quaternion.identity);
}

//public void SetItem(ItemSaveFile item)
//{
//    this.item = item;
//    spriteRenderer.sprite = ItemDirectory.GetItem(item.ID).icon;
//    textMesh.text = item.stacks > 1 ? item.stacks.ToString() : string.Empty;
//}

/*
 
    public Item() {}

    public Item(Item clone)
    {
        //This is for when we don't want references to mix
        this.ID             = clone.ID;
        this.itemName       = clone.itemName;
        this.description    = clone.description;
        //this.rarity       = clone.rarity;

        this.stacks         = clone.stacks;
        this.price          = clone.price;
        this.icon           = clone.icon;

        this.isStackable    = clone.isStackable;
        this.itemType       = clone.itemType;
    }

 
 */