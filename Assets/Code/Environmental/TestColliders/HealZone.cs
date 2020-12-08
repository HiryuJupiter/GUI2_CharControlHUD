using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyNameSpace
{
    public class HealZone : MonoBehaviour
    {
        LayerMask playerBodyLayer;
        PlayerController player;

        protected void Awake()
        {
            playerBodyLayer = CharacterSettings.instance.PlayerBodyLayer;
        }

        void Start()
        {
            //Reference
            player = PlayerController.Instance;
        }

        void OnTriggerEnter(Collider other)
        {
            //Heal the player when the player hits this trigger
            if (playerBodyLayer == (playerBodyLayer | 1 << other.gameObject.layer))
            {
                player.HealPlayer(999);
            }
        }

        //void OnTriggerExit(Collider other)
        //{
        //    if (playerBodyLayer == (playerBodyLayer | 1 << other.gameObject.layer))
        //    {
        //        UICanvas.SetActive(false);
        //        slotManager.SetIsOpen(false);
        //    }
        //}
    }

}