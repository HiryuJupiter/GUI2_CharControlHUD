using UnityEngine;
using UnityEngine.UI;
using System.Collections;



namespace MyNameSpace
{
    public class StatPageStatsWriter : MonoBehaviour
    {
        public static StatPageStatsWriter Instance;

        [Header("Primary info")]
        [SerializeField] Text PlayerName;
        [SerializeField] Text level;
        [SerializeField] Text profession;
        [SerializeField] Text race;

        [Header("Attributes")]
        [SerializeField] Text strength;
        [SerializeField] Text intelligence;
        [SerializeField] Text agility;
        [SerializeField] Text endurance;
        [SerializeField] Text willpower;
        [SerializeField] Text luck;

        [Header("Stats")]
        [SerializeField] Text HP;
        [SerializeField] Text HPRegen;
        [SerializeField] Text MP;
        [SerializeField] Text MPRegen;
        [SerializeField] Text AP;
        [SerializeField] Text APRegen;
        [Space(20)]
        [SerializeField] Text attackDamage;
        [SerializeField] Text abilityPower;
        [SerializeField] Text defence;
        [Space(20)]
        [SerializeField] Text evasionRate;
        [SerializeField] Text attackSpeed;
        [SerializeField] Text critChance;
        [SerializeField] Text moveSpeed;

        void Awake()
        {
            Instance = this;
        }

        public void InitializeAllInfo(GameData data)
        {
            PlayerName.text = data.playerName;
            UpdateLevel(data);
            profession.text = data.profession.ToString();
            race.text = data.race.ToString();

            UpdateAttributes(data.attributes);
            UpdateAllStats(data.stats);
        }

        public void UpdateLevel(GameData data)
        {
            level.text = data.level.ToString();
        }

        public void UpdateAttributes(PlayerAttribute a)
        {
            strength.text = a.Strength.ToString();
            intelligence.text = a.Intelligence.ToString();
            agility.text = a.Agility.ToString();
            endurance.text = a.Endurance.ToString();
            willpower.text = a.Willpower.ToString();
            luck.text = a.Luck.ToString();
        }

        public void UpdateAllStats(PlayerStats s)
        {
            HP.text = s.HP + " / " + s.HPMax;
            HPRegen.text = s.HPRegen.ToString();
            MP.text = s.MP + " / " + s.MPMax;
            MPRegen.text = s.MPRegen.ToString();
            AP.text = s.AP + " / " + s.APMax;
            APRegen.text = s.APRegen.ToString();

            attackDamage.text = s.AttackDamage.ToString();
            abilityPower.text = s.AbilityPower.ToString();
            defence.text = s.Defence.ToString();

            evasionRate.text = s.EvasionRate.ToString();
            attackSpeed.text = s.AttackSpeed.ToString();
            critChance.text = s.CritChance.ToString();
            moveSpeed.text = s.MoveSpeed.ToString();
        }
    }
}