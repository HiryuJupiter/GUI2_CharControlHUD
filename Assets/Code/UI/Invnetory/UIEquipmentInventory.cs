using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UIEquipmentInventory : UIInventory
{
    public static UIEquipmentInventory Instance;
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
            statWriter.InitializeAllInfo(player.data);
        }
    }
    #endregion

    public override void Initialize(SlotManager inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += RefreshInventoryDisplay;

        Slots[0].Initialize(inventory, 0);
        Slots[1].Initialize(inventory, 1);
        Slots[2].Initialize(inventory, 2);
        Slots[3].Initialize(inventory, 3);

        for (int i = 0; i < Slots.Count; i++)
        {
            Slots[i].Initialize(inventory, i);
        }

        //inventory.TryAddItem(new ItemSaveFile(0, 1));
        RefreshInventoryDisplay();
    }

    protected override void RefreshInventoryDisplay()
    {
        //Go through all slots and set them accordingly
        UpdateSlot(inventory.ItemList[0], Slots[0]);
        UpdateSlot(inventory.ItemList[1], Slots[1]);
        UpdateSlot(inventory.ItemList[2], Slots[2]);
        UpdateSlot(inventory.ItemList[3], Slots[3]);
    }

    void UpdateSlot (ItemSaveFile item, UIItemSlot uiSlot)
    {
        if (item == null || item.ID == ItemID.Empty) //If there is no item, then clear the slot
        {
            uiSlot.ClearSlot();
        }
        else
        {
            uiSlot.ForceSetItemInSlot(item);
        }
    }
}

/*
 UpdateSlot(GetInventoryItem(0), weaponLeft);
        UpdateSlot(GetInventoryItem(1), weaponRight);
        UpdateSlot(GetInventoryItem(2), armor);
        UpdateSlot(GetInventoryItem(3), pants);
 */