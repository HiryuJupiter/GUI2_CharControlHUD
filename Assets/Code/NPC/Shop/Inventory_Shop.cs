using UnityEngine;
using UnityEngine.UI;
using System.Collections;



namespace MyNameSpace
{//This is for chests and shops in the worldspace and are not tied to the save file. 
 //Inventory_Chest -> UISlotManager_WorldSpace
    public class Inventory_Shop : Inventory
    {
        public static Inventory_Shop Instance;

        [SerializeField] UISlotManagerBase slotManager;
        [SerializeField] GameObject UICanvas;
        LayerMask playerBodyLayer;
        float discountModifier = 1f;

        bool startedSelling;

        PlayerController player;

        void Awake()
        {
            Instance = this;
            playerBodyLayer = CharacterSettings.instance.PlayerBodyLayer;

            releasingCondition = (item, slotIndex) => player.TrySpendMoney((int)(item.price * 1.5f * discountModifier));
        }

        void Start()
        {
            //Reference
            player = PlayerController.Instance;
        }

        //Instead of initializing the shop in Awake, the shop inventoyr can be reinitialized endless times by endless NPCs
        public void NPCRequestOpenShop(NPC npc)
        {
            startedSelling = false;

            itemList = npc.ItemList;
            slotManager.Initialize(this);


            InvokeEvent_InventoryChange();
            startedSelling = true;
        }

        protected override void OnItemSlotted(int slotIndex)
        {
            if (startedSelling)
            {
                //Item sold, give player money
                player.AddMoney((int)(ItemDirectory.GetItem(itemList[slotIndex].ID).price * 0.5f));
            }
        }

        public void SetIsOpen(bool isOpen)
        {
            startedSelling = isOpen;
            UICanvas.SetActive(isOpen);
            slotManager.SetIsOpen(isOpen);
        }
    }
}