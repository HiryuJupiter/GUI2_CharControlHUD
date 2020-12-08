using UnityEngine;
using System.Collections;


namespace MyNameSpace
{
    public class PlayerModelPartSwapper : MonoBehaviour
    {
        public GameObject sword;
        public GameObject shield;


        public void SetWeaponVisibility(bool isVisible)
        {
            sword.SetActive(isVisible);
        }

        public void SetArmorVisibility(bool isVisible)
        {
            shield.SetActive(isVisible);
        }
    }
}