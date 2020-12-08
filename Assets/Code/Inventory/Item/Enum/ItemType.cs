using UnityEngine;
using System.Collections;

namespace MyNameSpace
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Consumable,
        QuestItem,
        Junk,
    }

    public static class ItemSortingMaps
    {
        public static int[] map_weaponFirst = new int[] { 0, 1, 2, 3, 4 };
        public static int[] map_armorFirst = new int[] { 1, 0, 2, 3, 4 };
        public static int[] map_ConsumableFirst = new int[] { 1, 2, 0, 3, 4 };
    }

    //You have to be extremely careful with this sorting map, because the result does not give you the ordering as displayed here in this list, as you can't do this:
    /*
      public static int[] map_weaponFirst = new int[]
    {
            (int) ItemType.Weapon,
            (int) ItemType.Armor,
            (int) ItemType.Consumable,
            (int) ItemType.Junk,
            (int) ItemType.QuestItem,
    };
     */
    //When you write: i.OrderBy(x => sortingMap[(int)(x.ItemType)]).ToList();
    //You are saying, if I put an integer value in here, what result do I get.
    //In more understandable terms, you are saying, instead of writing the desired sequence, you are individually sepcifying what the enum in these indexes should be positioned as. 
    //[3, 1,2, 4, 5] is not saying you want 3,3,3,1,1,1,2,2,4,4,5,5 as result, it is saying you want enum1 to be at position 3, enum2 to be at position 1, so you end up with enum2, enum3, enum1, enum4, enum5. 
}