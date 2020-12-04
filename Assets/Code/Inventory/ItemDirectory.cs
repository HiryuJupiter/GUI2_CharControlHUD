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

    public static ItemType GetItemType(ItemID ID)
    {
        return GetItem(ID).ItemType;
    }

    public static int GetItemTypeInt(ItemID ID)
    {
        return (int)GetItemType(ID);
    }

    public static void SpawnItemAtPlayerPosition(ItemSaveFile file)
    {
        SpawnItem(file, PlayerController.Instance.transform.position);
    }

    public static void SpawnItem (ItemSaveFile file, Vector3 position)
    {
        Vector3 p = new Vector3(position.x + Random.Range(-5f, 5f), position.y + 0.5f, position.z + Random.Range(-1f, 1f));
        Instantiate(GetItem(file.ID), p, Quaternion.identity).GetComponent<Item>().stacks = file.stacks;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            items[0].SpawnItem(Vector3.up);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnItemAtPlayerPosition(new ItemSaveFile(ItemID.weapon, 1));
        }
    }
}