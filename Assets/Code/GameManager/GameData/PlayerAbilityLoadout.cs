using UnityEngine;
using System.Collections;


namespace MyNameSpace
{
    [System.Serializable]
    public class PlayerAbilityLoadout
    {
        public AbilityTypes[] abilities = new AbilityTypes[3];

        public PlayerAbilityLoadout(RacialTypes race)
        {
            abilities[0] = AbilityTypes.Slash;
            abilities[1] = AbilityTypes.Whirl;

            SetRace(race);
        }

        public void SetRace(RacialTypes race)
        {
            switch (race)
            {
                case RacialTypes.FireBirds:
                    abilities[2] = AbilityTypes.FireLob;

                    break;
                case RacialTypes.IceFrogs:
                    abilities[2] = AbilityTypes.IcyBlast;
                    break;
            }
        }

        //Set profession
    }
}