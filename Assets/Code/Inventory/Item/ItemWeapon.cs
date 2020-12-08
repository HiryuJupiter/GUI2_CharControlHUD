using UnityEngine;
using System.Collections;


namespace MyNameSpace
{
    public class ItemWeapon : Item
    {
        public ItemWeapon()
        {
            isStackable = false;
            itemType = ItemType.Weapon;
        }

        public override void ItemSlotted()
        {

            PlayerController.Instance.SetWeaponVisibility(true);
        }

        public override void ItemUnslotted()
        {
            PlayerController.Instance.SetWeaponVisibility(false);
        }
    }
}