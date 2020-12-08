using UnityEngine;
using System.Collections;


namespace MyNameSpace
{
    public class UISlotManager_Generic : UISlotManagerBase
    {
        //The slots under this manager has no special properties, no hot keys to open.
        void Start()
        {
            SetIsOpen(false);
        }
    }
}