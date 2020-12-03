using UnityEngine;
using System.Collections;

public class ItemIconsDirectory : MonoBehaviour
{
    static ItemIconsDirectory Instance;

    public Sprite weaponIcon;
    public Sprite ArmorIcon;
    public Sprite ConsumableIcon;
    public Sprite QuestIcon;
    public Sprite JunkIcon;

    void Awake()
    {
        Instance = this;
    }

    Sprite Lookup(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Weapon: return weaponIcon;
            case ItemType.Armor: return ArmorIcon;
            case ItemType.Consumable: return ConsumableIcon;
            case ItemType.QuestItem: return QuestIcon;
            case ItemType.Junk:
            default: return JunkIcon;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        return Instance.Lookup(itemType);
    }
}