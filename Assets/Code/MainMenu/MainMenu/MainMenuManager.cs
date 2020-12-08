
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


namespace MyNameSpace
{
    [RequireComponent(typeof(SceneLoader))]
    public class MainMenuManager : MonoBehaviour
    {
        SceneLoader sceneLoader;
        const int SceneIndex_CharacterCreation = 1;
        const int SceneIndex_GamePlay = 2;

        //MainMenuState state = MainMenuState.MainMenu;

        //Class reference
        PersistentGameData gameData;
        SfxManager sfxManager;

        void Start()
        {
            sceneLoader = GetComponent<SceneLoader>();
            sfxManager = SfxManager.instance;

            gameData = PersistentGameData.Instance;
        }

        #region Public - Main menu
        public void StartNewGame()
        {
            //sceneLoader.LoadLevel(SceneIndex_CharacterCreation);
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneIndex_CharacterCreation);
            gameData.loadMode = LoadMode.StartNew;
        }

        public void ContinueGame()
        {
            //gameData.LoadPlayerDataOnSceneChange = true;
            gameData.loadMode = LoadMode.Load;
            //UnityEngine.SceneManagement.SceneManager.LoadScene(gameData.SaveFile.gameLevelIndex);        
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneIndex_GamePlay);
        }

        public void Clicked_Quit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }
        #endregion
    }
}