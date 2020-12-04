using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UIConsumableInventory : UIInventory
{
    public static UIConsumableInventory Instance;
    
    void Awake()
    {
        Instance = this;
    }
}