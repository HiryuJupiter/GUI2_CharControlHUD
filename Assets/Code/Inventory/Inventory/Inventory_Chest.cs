using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace MyNameSpace
{//This is for chests and shops in the worldspace and are not tied to the save file. 
 //Inventory_Chest -> UISlotManager_WorldSpace
    public class Inventory_Chest : Inventory
    {
        [SerializeField] UISlotManagerBase slotManager;
        [SerializeField] GameObject UICanvas;
        LayerMask playerBodyLayer;

        void Awake()
        {
            playerBodyLayer = CharacterSettings.instance.PlayerBodyLayer;
            //condition = (item, slotIndex) => item.ItemType == ItemType.Consumable;
        }

        void Start()
        {
            itemList = new ItemSaveFile[slotManager.SlotCount];

            slotManager.Initialize(this);


            TryPickUpItem(new ItemSaveFile(ItemID.potion, 3));
            TryPickUpItem(new ItemSaveFile(ItemID.weapon, 1));
            TryPickUpItem(new ItemSaveFile(ItemID.armor, 2));
            TryPickUpItem(new ItemSaveFile(ItemID.potion, 3));
        }

        void OnTriggerEnter(Collider other)
        {
            if (playerBodyLayer == (playerBodyLayer | 1 << other.gameObject.layer))
            {
                UICanvas.SetActive(true);
                slotManager.SetIsOpen(true);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (playerBodyLayer == (playerBodyLayer | 1 << other.gameObject.layer))
            {
                UICanvas.SetActive(false);
                slotManager.SetIsOpen(false);
            }
        }
    }
}