using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class GameData
{
    const int ConsumableBarSlots = 5;
    const int EquipmentSlots = 4;
    const int BagSlots = 8;

    public string playerName;

    //Level
    public int gameLevelIndex;
    public int spawnPointIndex;

    //Stats
    public int level;
    public int unusedStatPoints;
    public ProfessionTypes profession;
    public RacialTypes race;

    //Resources
    public int hp;
    public int mp;
    public int ap;
    public int money;

    //Attributes
    public PlayerAttribute attributes;
    public PlayerStats stats;
    public PlayerAbilityLoadout abilityLoadout;

    //public Colors
    public SerializableColor hairColor;
    public SerializableColor eyeColor;
    public SerializableColor headColor;
    public SerializableColor bodyColor;

    public ItemSaveFile[] bagItems;
    public ItemSaveFile[] consumableBarItems;
    public ItemSaveFile[] equipmentItems;

    int CurrentSceneIndex => UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    
    #region Public - IO
    public void SaveGameData()
    {
        gameLevelIndex = CurrentSceneIndex;
        GameDataIO.Save(this);
    }

    public void LoadGameData()
    {
        if (GameDataIO.TryLoad(this))
        {
            Debug.Log("+++++Player data loaded successfully");
        }       
        else
        {
            Debug.LogWarning("+++++Player data not found. Initializing to default.");
            InitalizeToDefault();
        }
    }

    public void ClearData ()
    {
        GameDataIO.ClearSave();
    }

    public void InitalizeToDefault()
    {

        playerName = "Please pass me";

        //Level
        gameLevelIndex = CurrentSceneIndex;
        spawnPointIndex = 0;

        //Stats
        level       = 1;
        profession  = ProfessionTypes.Warrior;
        race        = RacialTypes.FireBirds;

        attributes      = new PlayerAttribute(profession);
        stats           = new PlayerStats(attributes);
        abilityLoadout  = new PlayerAbilityLoadout(race);

        bagItems            = new ItemSaveFile[BagSlots];
        consumableBarItems  = new ItemSaveFile[ConsumableBarSlots];
        equipmentItems      = new ItemSaveFile[EquipmentSlots];
        ArrayPopulate(bagItems,            BagSlots);
        ArrayPopulate(consumableBarItems,  ConsumableBarSlots);
        ArrayPopulate(equipmentItems,      EquipmentSlots);

        hp = stats.HPMax;
        mp = stats.MPMax;
        ap = stats.APMax;
        money = 100;

        hairColor   = Color.red;
        eyeColor    = Color.yellow;
        headColor   = Color.white;
        bodyColor   = Color.blue;
    }
    #endregion

    void ArrayPopulate (ItemSaveFile[]  array, int length)
    {
        for (int i = 0; i < length; i++)
        {
            array[i] = new ItemSaveFile();
        }
    }
}