using UnityEngine;
using System.Collections;



namespace MyNameSpace
{
    public enum LoadMode { DoNothing, StartNew, Load }
    [DefaultExecutionOrder(-10)]
    public class PersistentGameData : MonoBehaviour
    {
        public static PersistentGameData Instance;

        public LoadMode loadMode = LoadMode.DoNothing;

        public GameData SaveFile { get; set; } = new GameData();

        void Awake()
        {
            //Singleton
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                LoadGame();
            }
            else
            {
                Destroy(gameObject);
            }

            //GameData.create
        }

        //Notes: Awake is called before OnLevelWasLoaded
        //This is not called in the first scene when we load up the game.
        void OnLevelWasLoaded(int level)
        {
            LoadGame();
        }

        void LoadGame()
        {
            Debug.Log("PersistentGameData + load game ");
            switch (loadMode)
            {
                case LoadMode.StartNew:

                    //Debug.Log(" Start new game data file");
                    SaveFile.InitalizeToDefault();
                    break;
                case LoadMode.Load:
                    //Debug.Log(" Load game data file");
                    SaveFile.LoadGameData();
                    break;
            }
        }
    }

}