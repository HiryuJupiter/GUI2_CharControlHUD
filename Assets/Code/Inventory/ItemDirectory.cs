using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//1   ~ 100 = Weapons
//101 ~ 200 = Armor
//201 ~ 300 = Consumable
//301 ~ 400 = Quest items
//401 ~ 500 = Junk

public class ItemDirectory : MonoBehaviour
{
    static ItemDirectory Instance;

    [SerializeField] Item[] items;

    Dictionary<ItemID, Item> lookup = new Dictionary<ItemID, Item>();

    void Awake()
    {
        Instance = this;

        foreach (var item in items)
        {
            lookup.Add(item.ID, item);
        }
    }

    public static Item GetItem (ItemID ID)
    {
        return Instance.lookup[ID];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            items[0].SpawnItem(Vector3.up);
        }
    }
}