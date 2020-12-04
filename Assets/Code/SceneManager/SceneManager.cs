using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    //Consts
    const int SceneIndex_MainMenu = 0;

    //Static
    public static SceneManager Instance;

    //References
    UIManager ui;
    PauseMenu pauseMenu;

    //State
    GameState[] states;

    //Properties
    public static GameStates gameState { get; set; } = GameStates.CharacterControl;

    #region MonoBehavior
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //Reference
        ui = UIManager.Instance;
        pauseMenu = PauseMenu.Instance;

        //Initialize UI display for HUD items
    }

    void Update()
    {
        //Toggle pause when player pressed Escape
        if ((Input.GetKeyDown(KeyCode.Escape)))
        {
            pauseMenu.TogglePause();
        }
    }
    #endregion

    #region Public - events
    public void Restart()
    {
        //Load current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMainMenu()
    {
        //Load main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneIndex_MainMenu);
    }
    #endregion
}