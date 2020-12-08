using UnityEngine;
using System.Collections;


namespace MyNameSpace
{
    public class ItemArmor : Item
    {
        public ItemArmor()
        {
            isStackable = false;
            itemType = ItemType.Armor;
        }

        public override void ItemSlotted()
        {

            PlayerController.Instance.SetArmorVisibility(true);
        }

        public override void ItemUnslotted()
        {
            PlayerController.Instance.SetArmorVisibility(false);
        }
    }
}