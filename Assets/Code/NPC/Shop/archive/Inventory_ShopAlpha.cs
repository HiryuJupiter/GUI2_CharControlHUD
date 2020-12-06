//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;

////This is for chests and shops in the worldspace and are not tied to the save file. 
////Inventory_Chest -> UISlotManager_WorldSpace
//public class Inventory_ShopAlpha : Inventory
//{
//    [SerializeField] UISlotManagerBase slotManager;
//    [SerializeField] GameObject UICanvas;
//    LayerMask playerBodyLayer;
//    float discountModifier = 1f;

//    bool startedSelling;

//    PlayerController player;

//    void Awake()
//    {
//        playerBodyLayer = CharacterSettings.instance.PlayerBodyLayer;

//        releasingCondition = (item, slotIndex) => player.TrySpendMoney((int)(item.price * 1.5f * discountModifier));
//    }

//    void Start()
//    {
//        //Reference
//        player = PlayerController.Instance;

//        //Initialize
//        itemList = new ItemSaveFile[slotManager.SlotCount];
//        slotManager.Initialize(this);

//        TryPickUpItem(new ItemSaveFile(ItemID.potion, 3));
//        TryPickUpItem(new ItemSaveFile(ItemID.potion, 3));
//        TryPickUpItem(new ItemSaveFile(ItemID.potion, 3));
//        TryPickUpItem(new ItemSaveFile(ItemID.weapon, 1));

//        startedSelling = true;
//    }

//    protected override void OnItemSlotted(int slotIndex)
//    {
//        if (startedSelling)
//        {
//            //Item sold, give player money
//            player.AddMoney((int)(ItemDirectory.GetItem(itemList[slotIndex].ID).price * 0.5f));
//        }
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        if (playerBodyLayer == (playerBodyLayer | 1 << other.gameObject.layer))
//        {
//            UICanvas.SetActive(true);
//            slotManager.SetIsOpen(true);
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (playerBodyLayer == (playerBodyLayer | 1 << other.gameObject.layer))
//        {
//            UICanvas.SetActive(false);
//            slotManager.SetIsOpen(false);
//        }
//    }

//}