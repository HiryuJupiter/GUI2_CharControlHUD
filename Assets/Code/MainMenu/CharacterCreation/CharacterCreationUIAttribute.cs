using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace MyNameSpace
{
    public class CharacterCreationUIAttribute : MonoBehaviour
    {
        public Text unspentPoints;

        //[Header("Primary info")]
        //[SerializeField] InputField PlayerName;
        //[SerializeField] Dropdown profession;
        //[SerializeField] Dropdown race;

        [Header("Attributes")]
        [SerializeField] Text strength;
        [SerializeField] Text intelligence;
        [SerializeField] Text agility;
        [SerializeField] Text endurance;
        [SerializeField] Text willpower;
        [SerializeField] Text luck;

        public void UpdateDisplay(GameData player)
        {
            //PlayerName.SetTextWithoutNotify(player.playerName);
            //profession.SetValueWithoutNotify((int)player.profession);
            //race.SetValueWithoutNotify((int)player.race);

            strength.text = player.attributes.Strength.ToString();
            intelligence.text = player.attributes.Intelligence.ToString();
            agility.text = player.attributes.Agility.ToString();
            endurance.text = player.attributes.Endurance.ToString();
            willpower.text = player.attributes.Willpower.ToString();
            luck.text = player.attributes.Luck.ToString();
        }

        public void SetUnspentPoints(int value) => unspentPoints.text = value.ToString();
    }
}