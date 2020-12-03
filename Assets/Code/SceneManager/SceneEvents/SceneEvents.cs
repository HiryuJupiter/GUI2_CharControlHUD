using UnityEngine;
using System.Collections;


public static class SceneEvents
{
    public static SceneEvent GameStart { get; set; } = new SceneEvent("Gamestart");

    public static SceneEvent PlayerSpawn { get; set; } = new SceneEvent("PlayerSpawn");
    public static SceneEvent PlayerDead { get; set; } = new SceneEvent("PlayerDead");
    public static SceneEvent GameQuit { get; set; } = new SceneEvent("GameQuit");

    //Game wide event
    public static SceneEvent GameSave { get; set; } = new SceneEvent("GameSave");
    public static SceneEvent GameLoad { get; set; } = new SceneEvent("GameLoad");

    public static bool GameWideEventsInitialized { get; set; }
    public static bool PerLevelEventsInitialized { get; set; }

    public static void UnSubscribePerLevelEvents ()
    {
        GameStart.UnSubscribeAll();
        PlayerSpawn.UnSubscribeAll();
        PlayerDead.UnSubscribeAll();
        GameQuit.UnSubscribeAll();
    }
}