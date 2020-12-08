using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyNameSpace
{
    [RequireComponent(typeof(PauseMenuCanvasTransition))]
    public class PauseMenu : MonoBehaviour
    {
        public static PauseMenu Instance;

        PauseMenuCanvasTransition transition;
        SceneManager sceneManager;
        SfxManager sfxManager;
        CursorManager cursorManager;
        PauseMenuStates state = PauseMenuStates.Unpaused;
        public bool isPaused { get; set; }

        #region MonoBehavior
        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            transition = GetComponent<PauseMenuCanvasTransition>();
            sceneManager = SceneManager.Instance;
            sfxManager = SfxManager.instance;
            cursorManager = CursorManager.Instance;

            UnPause();
        }

        void Update()
        {
            //Hotkey controls over options menu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                switch (state)
                {
                    case PauseMenuStates.Unpaused:
                        Pause();
                        break;
                    case PauseMenuStates.PauseMain:
                        UnPause();
                        break;
                    case PauseMenuStates.OptionsMenu:
                        OptionsToPauseMain();
                        break;
                }
                sfxManager.SpawnUI_1_Click();
            }
        }

        void OnDisable()
        {
            cursorManager.SetCursorToNormalArrow();
        }
        #endregion

        #region Public - Click unity events
        public void TogglePause()
        {
            if (isPaused = !isPaused)
                Pause();
            else
                UnPause();
        }

        public void Pause()
        {
            sfxManager.SpawnUI_1_Click();
            state = PauseMenuStates.PauseMain;
            transition.Pause();
            Time.timeScale = 0f;
            cursorManager.SetCursorToNormalArrow();
        }

        public void UIHandle_UnPause()
        {
            sfxManager.SpawnUI_1_Click();
            UnPause();
        }

        public void UIHandle_OpenOptionsMenu()
        {
            sfxManager.SpawnUI_1_Click();
            PauseMainToOptions();
        }

        public void UIHandle_CloseOptionsMenu()
        {
            sfxManager.SpawnUI_1_Click();
            OptionsToPauseMain();
        }

        public void UIHandle_QuitGame()
        {
            sfxManager.SpawnUI_1_Click();
            QuitGame();
        }
        #endregion

        #region Pause logic

        void PauseMainToOptions()
        {
            state = PauseMenuStates.OptionsMenu;
            transition.PauseMainToOptions();
        }

        void OptionsToPauseMain()
        {
            state = PauseMenuStates.PauseMain;
            transition.OptionsToPauseMain();
        }

        void UnPause()
        {
            state = PauseMenuStates.Unpaused;
            transition.UnPause();
            Time.timeScale = 1f;
            cursorManager.SetCursorToCrosshair();
        }

        void QuitGame()
        {
            sceneManager.ToMainMenu();
            UnPause();
        }
        #endregion
    }
}