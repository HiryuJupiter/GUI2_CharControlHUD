using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class SpriteAtlasLoader : MonoBehaviour
{
    public static SpriteAtlasLoader instance;

    Sprite[] sprites;
    Dictionary<string, Sprite> spritesDict = new Dictionary<string, Sprite>();

    void Awake()
    {
        instance = this;

        //Load atlas
        sprites = Resources.LoadAll<Sprite>("EventAtlas");

        //Initialize lookup
        foreach (Sprite s in sprites)
        {
            spritesDict.Add(s.name, s);
        }
    }

    public Sprite LoadSprite (string spriteName)
    {
        if (spritesDict.ContainsKey(spriteName))
        {
            return spritesDict[spriteName];
        }
        else
        {
            Debug.LogError($"spriteName: '{spriteName}' does not exist");
            return null;
        }
    }
}