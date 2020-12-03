﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterCreationUIAttribute))]
[RequireComponent(typeof(CharacterCretionColorizer))]
public class CharacterCreator : MonoBehaviour
{
    const int MaxUnspentStatPoints = 10;
    const int GameplaySceneIndex = 2;

    //Ref
    CharacterCreationUIAttribute attributes;
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
        attributes = GetComponent<CharacterCreationUIAttribute>();
    }

    void Start()
    {
        //Reference
        gameData = PersistentGameData.Instance;

        //Initalize ui attributes display
        attributes.UpdateDisplay(gameData.SaveFile);
        attributes.SetUnspentPoints(unspentStatPoints);
    }

    public void ConfirmCreation ()
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

        attributes.UpdateDisplay(gameData.SaveFile);
        attributes.SetUnspentPoints(unspentStatPoints);
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
            attributes.SetUnspentPoints(--unspentStatPoints);
            gameData.SaveFile.attributes.SetAttribute(type, stat + 1);
            attributes.UpdateDisplay(gameData.SaveFile);
        }
        else if (stat > 0)
        {
            attributes.SetUnspentPoints(++unspentStatPoints);
            gameData.SaveFile.attributes.SetAttribute(type, stat - 1);
            attributes.UpdateDisplay(gameData.SaveFile);
        }
    }
    #endregion
}