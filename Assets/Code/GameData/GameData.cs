using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class GameData
{
    const int ConsumableBarSlots = 5;
    const int InventorySlots = 8;

    public string playerName;

    //Level
    public int gameLevelIndex;
    public int spawnPointIndex;

    //Stats
    public int level;
    public int unusedStatPoints;
    public ProfessionTypes profession;
    public RacialTypes race;

    //Attributes
    public PlayerAttribute attributes;
    public PlayerStats stats;
    public PlayerAbilityLoadout abilityLoadout;

    //public Colors
    public SerializableColor hairColor;
    public SerializableColor eyeColor;
    public SerializableColor headColor;
    public SerializableColor bodyColor;

    public Inventory inventory;
    public Inventory consumableBarInventory;
    public WearingInventory equipmentInventory;

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
            Debug.Log("Player data loaded successfully");
        }       
        else
        {
            Debug.LogWarning("Player data not found. Initializing to default.");
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

        inventory               = new Inventory(InventorySlots, null);
        consumableBarInventory  = new Inventory(ConsumableBarSlots, x => x.itemType == ItemType.Consumable);
        equipmentInventory      = new WearingInventory();

        hairColor   = Color.red;
        eyeColor    = Color.yellow;
        headColor   = Color.white;
        bodyColor   = Color.blue;
    }
    #endregion
}