using UnityEngine;
using System.Collections;

//[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public abstract class  Item : MonoBehaviour
{
    protected float cooldownDuration;

    public ItemID ID;
    public string itemName;
    public string description;

    public int price = 1;
    public Sprite icon;

    [HideInInspector] public int stacks = 1;

    //Stats
    protected bool isStackable;
    protected ItemType itemType;

    //Reference
    protected SpriteRenderer spriteRenderer;
    protected TextMesh textMesh;

    //Property
    public bool IsStackable => isStackable;
    public ItemType ItemType => itemType;
    public static float CooldownTimer { get; protected set; }
    public float CooldownPercent => (cooldownDuration <= 0) ? 0f : CooldownTimer / cooldownDuration;
    public bool HasCooldown => cooldownDuration > 0.01f;
    public bool CooldownReady => CooldownTimer <= 0f;

    #region MonoBehavior
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Collider>().enabled = true;
    }

    void Update()
    {
        transform.rotation = GlobalRotation.CoinRotation;
    }
    #endregion

    #region Public

    public virtual bool TryUseItem() => false; //Items cannot be used by default

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public GameObject SpawnItem(Vector3 position) => Instantiate(gameObject, position, Quaternion.identity);


    //protected override void ItemSlotted(int slotIndex) => GetItemFromID(itemList[slotIndex].ID).ItemUnslotted();

    public virtual void ItemSlotted() { }
    public virtual void ItemUnslotted() { }
    #endregion

    #region Private
    protected IEnumerator StartCDTimer ()
    {
        CooldownTimer = cooldownDuration;
        while (CooldownTimer > 0f)
        {
            CooldownTimer -= Time.deltaTime;
            yield return null;
        }
    }
    #endregion

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