using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



namespace MyNameSpace
{//Responsible for updating the UI group's content based on the quest fed in here
    public class UIQuestScreenManager : CanvasPageToggle
    {
        public static UIQuestScreenManager Instance { get; private set; }

        [SerializeField] List<Image> rewardIcons;



        void Awake()
        {
            Instance = this;
        }


        public void UpdateQuest(Quest quest)
        {
            //Go through all the reward icon and fill them on screen
            for (int i = 0; i < rewardIcons.Count; i++)
            {
                if (i < quest.ItemRewards.Length)
                {
                    rewardIcons[i].sprite = ItemDirectory.GetItem(quest.ItemRewards[i]).icon;
                    rewardIcons[i].enabled = true;
                }
                else
                {
                    rewardIcons[i].enabled = false;
                }
            }
        }


        public override void SetIsOpen(bool isOpen)
        {
            this.isOpen = isOpen;
            if (isOpen)
            {
                CanvasGroupUtil.InstantReveal(canvasGroup);
                RefreshPanel();
            }
            else
            {
                CanvasGroupUtil.InstantHide(canvasGroup);
            }
        }

        void RefreshPanel()
        {

        }

    }
}