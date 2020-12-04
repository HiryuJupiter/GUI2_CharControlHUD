using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Test_ClassReference : MonoBehaviour
{
    //equipmentItems      = new ItemSaveFile[EquipmentSlots];
    class Test1
    {
        public int i = 2;

        public Test1()
        {

        }

        public Test1(int i)
        {
            this.i = i;
        }
    }

    void Start()
    {
        //ItemSaveFile[] simps1 = new ItemSaveFile[3];

        //Debug.Log("" + (simps1[0] == null) );
        //Debug.Log("" + (simps1[0] == null) );
        //Debug.Log("" + (simps1[0] == null) );
        //Debug.Log("" + (simps1[0] == null) );
        //Debug.Log("" + (simps1[0] == null) );

        List<int> list = new List<int> { 2, 3, 5, 6, 7, 5, 33, 1, 2 };
        int[] sortingMap = new int[] { 2, 3, 6, 7 };
        list.OrderBy(x => sortingMap[x]);

        foreach (var item in list)
        {

            Debug.Log(" :" + item);
        }
    }
}
