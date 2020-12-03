using UnityEngine;
using System.Collections;
using System.Linq;

public static class ItemSortingMaps
{
    public static int[] map_weaponFirst = new int[]
    {
            (int) ItemType.Weapon,
            (int) ItemType.Armor,
            (int) ItemType.Consumable,
            (int) ItemType.Junk,
            (int) ItemType.QuestItem,
    };

    public static int[] map_armorFirst = new int[]
    {
            (int) ItemType.Armor,
            (int) ItemType.Weapon,
            (int) ItemType.Consumable,
            (int) ItemType.Junk,
            (int) ItemType.QuestItem,
    };

    public static int[] map_ConsumableFirst = new int[]
    {
            (int) ItemType.Consumable,
            (int) ItemType.Weapon,
            (int) ItemType.Armor,
            (int) ItemType.Junk,
            (int) ItemType.QuestItem,
    };
}