using UnityEngine;
using System.Collections;

//World-space items.
public class PickupItem : MonoBehaviour
{
    Item item;
    SpriteRenderer spriteRenderer;
    TextMesh textMesh;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public Item GetItem ()
    {
        return item;
    }

    public void SetItem (Item item)
    {
        this.item = item;
        spriteRenderer.sprite = ItemIconsDirectory.GetSprite(item.itemType);
        textMesh.text = item.amount > 1 ? item.amount.ToString() : string.Empty;
    }

    public void DestroySelf ()
    {
        Destroy(gameObject);
    }
}