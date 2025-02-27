﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace MyNameSpace
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(CustomUIButtonColorTint))]
    public class ContinueButton : MonoBehaviour
    {
        Button button;
        Image img;
        CustomUIButtonColorTint colorTint;

        void Awake()
        {
            button = GetComponent<Button>();
            img = GetComponent<Image>();
            colorTint = GetComponent<CustomUIButtonColorTint>();
        }

        void Start()
        {
            RefreshDisplay();
        }

        public void RefreshDisplay()
        {
            if (GameDataIO.HasSaveFile())
            {
                //EnableButton();
            }
            else
            {
                GreyOutButton();
            }
        }

        void EnableButton()
        {
            colorTint.DisableButton();
            //button.enabled = true;
            //img.color = Color.white;
        }

        void GreyOutButton()
        {
            colorTint.DisableButton();
            //button.enabled = false;
            //img.color = Color.grey;
        }
    }
}