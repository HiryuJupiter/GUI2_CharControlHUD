using UnityEngine;
using System.Collections;

public class GlobalRotation : MonoBehaviour
{
    public static Quaternion CoinRotation;

    public float CoinRotationModifier = 0.5f;

    void Update()
    {
        CoinRotation = Quaternion.Euler(0f, Time.time * CoinRotationModifier, 0f);
    }

    //private void OnGUI()
    //{
    //    //GUI.Label(new Rect(20, 20, 500, 20), "" + );
    //}
}