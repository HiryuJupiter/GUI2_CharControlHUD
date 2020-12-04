using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UISlotManager_ConsumableBar : UISlotManagerBase
{
    public static UISlotManager_ConsumableBar Instance;

    void Awake()
    {
        SetIsOpen(true);
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TryUseItemInSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TryUseItemInSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TryUseItemInSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TryUseItemInSlot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TryUseItemInSlot(4);
        }
    }

    void TryUseItemInSlot (int itemSlot)
    {
        try
        {
            Slots[itemSlot].UseItem();
        }
        catch (System.Exception e)
        {

            Debug.Log(" Exception. Why is there an error. /weep " + e);
        }
    }
}