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
        isOpen = true;
        Instance = this;
    }
}