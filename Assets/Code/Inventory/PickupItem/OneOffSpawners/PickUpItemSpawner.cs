using UnityEngine;
using System.Collections;

public class PickUpItemSpawner : MonoBehaviour
{
    public static PickUpItemSpawner Instance;

    public PickupItem pfWorldItem;

    public static PickupItem SpawnWorldItem(Vector3 position, Item item)
    {
        PickupItem i = Instantiate(Instance.pfWorldItem, position, Quaternion.identity).GetComponent<PickupItem>();
        i.SetItem(item);
        return i;
    }

    void Awake()
    {
        Instance = this;
    }
}