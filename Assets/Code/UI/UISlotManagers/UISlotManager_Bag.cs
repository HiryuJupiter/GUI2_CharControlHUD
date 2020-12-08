using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



namespace MyNameSpace
{
    public class UISlotManager_Bag : UISlotManagerBase
    {
        public static UISlotManager_Bag Instance;

        #region MonoBehavior
        void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleOpen();
            }
        }
        #endregion

        #region Public - set sorting mode
        public void SortByWeapon()
        {
            sortOn = true;
            sortingMap = ItemSortingMaps.map_weaponFirst;
            RefreshInventoryDisplay();
            SortByNone();
        }

        public void SortByArmor()
        {
            sortOn = true;
            sortingMap = ItemSortingMaps.map_armorFirst;
            RefreshInventoryDisplay();
            SortByNone();
        }

        public void SortByConsumable()
        {
            sortOn = true;
            sortingMap = ItemSortingMaps.map_ConsumableFirst;
            RefreshInventoryDisplay();
            SortByNone();
        }

        public void SortByNone() => sortOn = false;
        #endregion
    }

}