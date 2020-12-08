using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;



namespace MyNameSpace
{
    public class ButtonBorderDisplayer : MonoBehaviour
    {
        //Variables
        [SerializeField] Image[] buttonBorders;
        int currentlyRevealed = -1;
        Dictionary<AbilityTypes, int> ablityImage;

        void Awake()
        {
            //Look up table
            ablityImage = new Dictionary<AbilityTypes, int>()
        {
            {AbilityTypes.Slash, 0 },
            {AbilityTypes.Whirl, 1 },
        };

            //Hide all borders
            foreach (var b in buttonBorders)
            {
                b.enabled = false;
            }
        }

        #region Public
        public void EnterPlacementMode(AbilityTypes ability)
        {
            //First hide currently revealed borders then revela the new one
            HideCurrentBorder();
            RevealButtonBorder(ablityImage[ability]);
        }

        public void ExitPlacementMode()
        {
            HideCurrentBorder();
        }

        #endregion
        void RevealButtonBorder(int buttonIndex)
        {
            //Hide the currently active border then reveal the new highlight border
            HideCurrentBorder();

            SetBorderVisibility(buttonIndex, true);
        }

        void HideCurrentBorder()
        {
            //If we're currently in a spawning state, then a border is active and we will now deactivate it. 
            if (currentlyRevealed != -1)
            {
                SetBorderVisibility(currentlyRevealed, false);
                currentlyRevealed = -1;
            }
        }

        void SetBorderVisibility(int index, bool isVisible)
        {
            //Set border's visibility 
            buttonBorders[index].enabled = isVisible;
            if (isVisible)
            {
                currentlyRevealed = index;
            }
        }
    }
}