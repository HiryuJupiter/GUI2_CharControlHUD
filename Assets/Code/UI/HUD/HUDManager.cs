﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;



namespace MyNameSpace
{
    [RequireComponent(typeof(OnHurtHUDBorder))]
    public class HUDManager : MonoBehaviour
    {
        [Header("Group reference")]
        [SerializeField] GameObject HUD_Group;

        [Header("Image bars")]
        [SerializeField] Image healthBar;
        [SerializeField] Image manaBar;
        [SerializeField] Image staminaBar;

        [Header("Money")]
        [SerializeField] Text money;

        [Header("Color gradient")]
        [SerializeField] Gradient gradient;

        [Header("Quest")]
        [SerializeField] Text ActiveQuest;

        OnHurtHUDBorder hurtBorder;

        void Awake()
        {
            hurtBorder = GetComponent<OnHurtHUDBorder>();
        }

        #region HP MP AP
        public void SetHealthBar(float percentage)
        {
            healthBar.fillAmount = Mathf.Clamp01(percentage);
        }

        public void SetManaBar(float percentage) => manaBar.fillAmount = Mathf.Clamp01(percentage);

        public void SetStaminaBar(float percentage) => staminaBar.fillAmount = Mathf.Clamp01(percentage);

        #endregion

        public void SetActiveQuest(Quest quest)
        {
            ActiveQuest.text = quest.Description;
        }

        public void ClearActiveQuest(Quest quest)
        {
            ActiveQuest.text = "";
        }

        public void SetMoney(int amount)
        {
            money.text = amount.ToString();
        }

        public void FlashDamageBorder()
        {
            hurtBorder.FlashRed();
        }

        public void SetIsVisible(bool isVisible) => HUD_Group.SetActive(isVisible);
    }

    //     healthBar.color = gradient.Evaluate(percentage);
}