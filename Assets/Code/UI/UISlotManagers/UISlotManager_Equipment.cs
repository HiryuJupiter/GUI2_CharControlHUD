using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace MyNameSpace
{
    public class UISlotManager_Equipment : UISlotManagerBase
    {
        public static UISlotManager_Equipment Instance;
        public Camera equipmentCamera;

        StatPageStatsWriter statWriter;
        PlayerController player;

        //Item GetInventoryItem(int index) => ItemDirectory.GetItem(inventory.ItemList[0].ID);

        #region MonoBehavior
        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            statWriter = StatPageStatsWriter.Instance;
            player = PlayerController.Instance;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                ToggleOpen();
            }
        }
        #endregion

        public override void ToggleOpen()
        {
            SetIsOpen(isOpen = !isOpen);
            equipmentCamera.enabled = isOpen;
            statWriter.InitializeAllInfo(player.data);

        }



        //#region Outputs only, from Inventory -> UI
        //protected override void RefreshInventoryDisplay()
        //{

    }//    if (!isOpen) return;

    //    //Go through all slots and set them accordingly
    //    UpdateSlot(inventory.ItemList[0], Slots[0]); //Weapon left
    //    UpdateSlot(inventory.ItemList[1], Slots[1]); //Weapon right
    //    UpdateSlot(inventory.ItemList[2], Slots[2]); //Armor
    //    UpdateSlot(inventory.ItemList[3], Slots[3]); //Armor
    //}

    //void UpdateSlot(ItemSaveFile item, UIItemSlot uiSlot)
    //{
    //    if (item == null || item.ID == ItemID.Empty) //If there is no item, then clear the slot
    //    {
    //        Debug.Log(" X");

    //        uiSlot.ClearSlotWithoutNotifyingInventory();
    //    }
    //    else
    //    {
    //        Debug.Log(" Y");
    //        uiSlot.SetSlotWithoutNotifyingInventory(item);
    //    }
    //}
    //#endregion
}

/*
 UpdateSlot(GetInventoryItem(0), weaponLeft);
        UpdateSlot(GetInventoryItem(1), weaponRight);
        UpdateSlot(GetInventoryItem(2), armor);
        UpdateSlot(GetInventoryItem(3), pants);
 */