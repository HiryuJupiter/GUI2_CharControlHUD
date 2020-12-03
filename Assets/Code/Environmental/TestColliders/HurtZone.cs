using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtZone : MonoBehaviour
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
            other.GetComponent<PlayerController>().DamagePlayer(transform.position, 50);

            Debug.Log(" Player walked into Hurt Zone");
        }
    }
}
