using UnityEngine;
using System.Collections;



namespace MyNameSpace
{
    [RequireComponent(typeof(CharacterCreationUIAttribute))]
    [RequireComponent(typeof(CharacterCretionColorizer))]
    public class CharacterCreator : MonoBehaviour
    {
        const int MaxUnspentStatPoints = 10;
        const int GameplaySceneIndex = 2;

        //Ref
        CharacterCreationUIAttribute attributesUI;
        PersistentGameData gameData;
        CharacterCretionColorizer colorer;

        //Status
        int unspentStatPoints;

        void Awake()
        {
            //Initialize
            unspentStatPoints = MaxUnspentStatPoints;

            //Reference
            colorer = GetComponent<CharacterCretionColorizer>();
            attributesUI = GetComponent<CharacterCreationUIAttribute>();
        }

        void Start()
        {
            //Reference
            gameData = PersistentGameData.Instance;

            //Initalize ui attributes display
            attributesUI.UpdateDisplay(gameData.SaveFile);
            attributesUI.SetUnspentPoints(unspentStatPoints);
        }

        public void ConfirmCreation()
        {
            //Save all settings
            gameData.SaveFile.hairColor = colorer.hair.material.color;
            gameData.SaveFile.eyeColor = colorer.eyeL.material.color;
            gameData.SaveFile.headColor = colorer.head.material.color;
            gameData.SaveFile.bodyColor = colorer.body.material.color;
            gameData.SaveFile.SaveGameData();
            gameData.loadMode = LoadMode.Load;

            //Debug.Log(" :" + gameData.Player.abilityLoadout.abilities[2]);

            //Must save game data to a binary file for assessment
            UnityEngine.SceneManagement.SceneManager.LoadScene(GameplaySceneIndex);
        }

        #region Set Color
        public void ModifyHairColor(bool isIncrement)
        {
            colorer.ColorHair(isIncrement ? 1 : -1);
            gameData.SaveFile.hairColor = colorer.hair.material.color;
        }
        public void ModifyEyeColor(bool isIncrement)
        {
            colorer.ColorEyes(isIncrement ? 1 : -1);
            gameData.SaveFile.eyeColor = colorer.eyeL.material.color;
        }
        public void ModifyHeadColor(bool isIncrement)
        {
            colorer.ColorHead(isIncrement ? 1 : -1);
            gameData.SaveFile.headColor = colorer.head.material.color;
        }
        public void ModifyBodyColor(bool isIncrement)
        {
            colorer.ColorBody(isIncrement ? 1 : -1);
            gameData.SaveFile.bodyColor = colorer.body.material.color;
        }
        #endregion

        #region Primary stats
        public void SetCharacterName(string s) => gameData.SaveFile.playerName = s;
        public void SetRace(int index)
        {
            gameData.SaveFile.race = (RacialTypes)index;
            gameData.SaveFile.abilityLoadout.SetRace(gameData.SaveFile.race);
        }
        #endregion

        #region Profession
        public void SetProfession(int professionIndex)
        {
            ProfessionTypes profession = (ProfessionTypes)professionIndex;
            gameData.SaveFile.profession = profession;
            gameData.SaveFile.attributes.SetToProfessionTemplate(profession);
            unspentStatPoints = MaxUnspentStatPoints;

            attributesUI.UpdateDisplay(gameData.SaveFile);
            attributesUI.SetUnspentPoints(unspentStatPoints);
        }

        public void ModifyStrength(bool isIncrement) => ModifyAttribute(isIncrement, AttributeTypes.Strength);
        public void ModifyIntelligence(bool isIncrement) => ModifyAttribute(isIncrement, AttributeTypes.Intelligence);

        public void ModifyAgility(bool isIncrement) => ModifyAttribute(isIncrement, AttributeTypes.Agility);

        public void ModifyEndurance(bool isIncrement) => ModifyAttribute(isIncrement, AttributeTypes.Endurance);

        public void ModifyWillpower(bool isIncrement) => ModifyAttribute(isIncrement, AttributeTypes.Willpower);

        public void ModifyLuck(bool isIncrement) => ModifyAttribute(isIncrement, AttributeTypes.Luck);

        void ModifyAttribute(bool isIncrement, AttributeTypes type)
        {
            int stat = gameData.SaveFile.attributes.GetAttribute(type);
            if (isIncrement && unspentStatPoints > 0)
            {
                attributesUI.SetUnspentPoints(--unspentStatPoints);
                gameData.SaveFile.attributes.SetAttribute(type, stat + 1);
                attributesUI.UpdateDisplay(gameData.SaveFile);
            }
            else if (stat > 0)
            {
                attributesUI.SetUnspentPoints(++unspentStatPoints);
                gameData.SaveFile.attributes.SetAttribute(type, stat - 1);
                attributesUI.UpdateDisplay(gameData.SaveFile);
            }
        }
        #endregion

        void OnGUI()
        {
            return;
            if (GUI.Button(new Rect(20, 300, 100, 20), "<"))
            {
                ModifyHairColor(false);
            }
            if (GUI.Button(new Rect(20, 370, 100, 20), "<"))
            {
                ModifyEyeColor(false);
            }
            if (GUI.Button(new Rect(20, 440, 100, 20), "<"))
            {
                ModifyHeadColor(false);
            }
            if (GUI.Button(new Rect(20, 510, 100, 20), "<"))
            {
                ModifyBodyColor(false);
            }

            if (GUI.Button(new Rect(400, 300, 100, 20), ">"))
            {
                ModifyHairColor(true);
            }
            if (GUI.Button(new Rect(400, 370, 100, 20), ">"))
            {
                ModifyEyeColor(true);
            }
            if (GUI.Button(new Rect(400, 440, 100, 20), ">"))
            {
                ModifyHeadColor(true);
            }
            if (GUI.Button(new Rect(400, 510, 100, 20), ">"))
            {
                ModifyBodyColor(true);
            }


            if (GUI.Button(new Rect(1550, 420, 100, 20), "-"))
            {
                ModifyStrength(false);
            }
            if (GUI.Button(new Rect(1550, 520, 100, 20), "-"))
            {
                ModifyIntelligence(false);
            }
            if (GUI.Button(new Rect(1550, 620, 100, 20), "-"))
            {
                ModifyAgility(false);
            }
            if (GUI.Button(new Rect(1550, 720, 100, 20), "-"))
            {
                ModifyEndurance(false);
            }
            if (GUI.Button(new Rect(1550, 820, 100, 20), "-"))
            {
                ModifyWillpower(false);
            }
            if (GUI.Button(new Rect(1550, 920, 100, 20), "-"))
            {
                ModifyLuck(false);
            }

            if (GUI.Button(new Rect(1800, 420, 100, 20), "+"))
            {
                ModifyStrength(true);
            }
            if (GUI.Button(new Rect(1800, 520, 100, 20), "+"))
            {
                ModifyIntelligence(true);
            }
            if (GUI.Button(new Rect(1800, 620, 100, 20), "+"))
            {
                ModifyAgility(true);
            }
            if (GUI.Button(new Rect(1800, 720, 100, 20), "+"))
            {
                ModifyEndurance(true);
            }
            if (GUI.Button(new Rect(1800, 820, 100, 20), "+"))
            {
                ModifyWillpower(true);
            }
            if (GUI.Button(new Rect(1800, 920, 100, 20), "+"))
            {
                ModifyLuck(true);
            }
        }
    }
}