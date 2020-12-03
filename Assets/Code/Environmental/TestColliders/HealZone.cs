using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{
    LayerMask playerBodyLayer;

    protected void Awake()
    {
        playerBodyLayer = CharacterSettings.instance.PlayerBodyLayer;
    }

    void OnTriggerEnter(Collider other)
    {
        if (playerBodyLayer == (playerBodyLayer | 1 << other.gameObject.layer))
        {
            other.GetComponent<PlayerController>().HealPlayer(999);
        }
    }
}
