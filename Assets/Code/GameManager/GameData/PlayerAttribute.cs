/*
 Attributes cannot be increased by equipments, only by leveling up.
 */




namespace MyNameSpace
{
    public enum AttributeTypes
    {
        Strength,       // + AttackDamage
        Intelligence,   // + AbilityPower
        Agility,        // + AttackSpeed, movespeed
        Endurance,      // + maxHP, HPRegen, defence
        Willpower,      // + maxMP, MPRegen, MaxAP, APRegen
        Luck            // + evasion rate and crit chance
    }

    [System.Serializable]
    public class PlayerAttribute
    {
        public int Strength = 1;
        public int Intelligence = 1;
        public int Endurance = 1;
        public int Agility = 1;
        public int Willpower = 1;
        public int Luck = 1;

        public PlayerAttribute(ProfessionTypes profession = ProfessionTypes.Warrior)
        {
            SetToProfessionTemplate(profession);
        }

        public void SetToProfessionTemplate(ProfessionTypes profession)
        {
            switch (profession)
            {
                case ProfessionTypes.Warrior:
                    Strength = 9;
                    Intelligence = 1;
                    Endurance = 4;
                    Agility = 3;
                    Willpower = 1;
                    Luck = 2;
                    break;
                case ProfessionTypes.Knight:
                    Strength = 6;
                    Intelligence = 2;
                    Endurance = 8;
                    Agility = 2;
                    Willpower = 1;
                    Luck = 1;
                    break;
                case ProfessionTypes.Priest:
                    Strength = 1;
                    Intelligence = 5;
                    Endurance = 2;
                    Agility = 2;
                    Willpower = 7;
                    Luck = 3;


                    break;
                case ProfessionTypes.Mage:
                    Strength = 1;
                    Intelligence = 8;
                    Endurance = 2;
                    Agility = 2;
                    Willpower = 4;
                    Luck = 3;
                    break;
                case ProfessionTypes.Archer:
                    Strength = 1;
                    Intelligence = 1;
                    Endurance = 2;
                    Agility = 7;
                    Willpower = 4;
                    Luck = 5;
                    break;
                case ProfessionTypes.Assassin:
                default:
                    Strength = 5;
                    Intelligence = 1;
                    Endurance = 2;
                    Agility = 4;
                    Willpower = 2;
                    Luck = 6;
                    break;
            }
        }

        public int GetAttribute(AttributeTypes attribute)
        {
            switch (attribute)
            {
                case AttributeTypes.Strength: return Strength;
                case AttributeTypes.Intelligence: return Intelligence;
                case AttributeTypes.Agility: return Endurance;
                case AttributeTypes.Endurance: return Agility;
                case AttributeTypes.Willpower: return Willpower;
                case AttributeTypes.Luck:
                default: return Luck;
            }
        }

        public void SetAttribute(AttributeTypes attribute, int value)
        {
            switch (attribute)
            {
                case AttributeTypes.Strength: Strength = value; break;
                case AttributeTypes.Intelligence: Intelligence = value; break;
                case AttributeTypes.Agility: Endurance = value; break;
                case AttributeTypes.Endurance: Agility = value; break;
                case AttributeTypes.Willpower: Willpower = value; break;
                case AttributeTypes.Luck:
                default: Luck = value; break;
            }
        }
    }


}