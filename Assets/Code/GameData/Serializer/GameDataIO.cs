using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//This was set up so that the player will have no knowledge of the middleman 
//GameDataSerializable
public static class GameDataIO
{
    static string SavePath => Application.persistentDataPath + "/player.save";

    public static void Save(GameData gameData)
    {
        //Using statement to automatically close FileStream when code goes out of scope
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream file = File.Create(SavePath))
        {
            //Convert the object into a binary file
            formatter.Serialize(file, gameData);
        }

        //Alternative method
        //FileStream stream = new FileStream(SavePath, FileMode.Create);
        //formatter.Serialize(stream, gameData);
        //stream.Close();
    }

    public static bool TryLoad(GameData gameData)
    {
        if (HasSaveFile())
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream file = File.Open(SavePath, FileMode.Open))
            {
                //Deserialization returns an Object type, so we have to manually cast it.
                gameData = (GameData)formatter.Deserialize(file);
            }

            //Alternative method
            //FileStream stream = new FileStream(SavePath, FileMode.Open);
            //gameData = formatter.Deserialize(stream) as PlayerData;
            //stream.Close();

            return true;
        }
        else
        {
            Debug.LogWarning("Save file does not exist.");
            return false;
        }
    }

    public static void ClearSave()
    {
        if (HasSaveFile())
        {
            File.Delete(SavePath);
        }
    }
    public static bool HasSaveFile() => File.Exists(SavePath);
}