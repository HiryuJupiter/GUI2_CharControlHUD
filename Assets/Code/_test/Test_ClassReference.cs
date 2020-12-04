using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Test_ClassReference : MonoBehaviour
{
    //equipmentItems      = new ItemSaveFile[EquipmentSlots];
    class DailyItemClass
    {
        public DailyItems itemType;
        public DailyItemClass(DailyItems itemType)
        {
            this.itemType = itemType;
        }
    }

    enum DailyItems { cat, fridge, car, box, iron, pizza}

    List<DailyItemClass> list = new List<DailyItemClass>
    {
        new DailyItemClass(DailyItems.car),
        new DailyItemClass(DailyItems.fridge),
        new DailyItemClass(DailyItems.car),
        new DailyItemClass(DailyItems.cat),
        new DailyItemClass(DailyItems.fridge),
    };

    void Start()
    {
        Debug.Log("no sorting");
        foreach (var item in list)
        {
            Debug.Log(item.itemType);
        }
        Debug.Log("----------");

        
        list = list.OrderBy(x => sortingMap2[(int)x.itemType]).ToList();
        //list.OrderBy(x => sortingMap[x]);

        foreach (var item in list)
        {
            Debug.Log(" :" + item.itemType);
        }

        //Debug.Log("sss :" + sortingMap[2]);
    }

    int[] sortingMap = new int[]
        {
            (int) DailyItems.car,
            (int) DailyItems.fridge,
            (int) DailyItems.iron,
            (int) DailyItems.cat,
            (int) DailyItems.pizza,
        };

    int[] sortingMap2 = new int[]
    {
            (int) DailyItems.pizza,
            (int) DailyItems.car,
            (int) DailyItems.fridge,
            (int) DailyItems.cat,
            (int) DailyItems.iron,
    };
}
