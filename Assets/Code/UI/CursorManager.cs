using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;
    static EventSystem eventSystem;

    public Texture2D NormalArrow;
    public Texture2D Crosshair;
    public static bool IsMouseOverUI { get; set; }

    void Awake()
    {
        Instance = this;
        eventSystem = EventSystem.current;
    }

    void Update()
    {
        //Checks if we are mover over UI element
        IsMouseOverUI = eventSystem.IsPointerOverGameObject();
    }

    public void SetCursorToCrosshair ()
    {
        Cursor.SetCursor(Crosshair, Vector2.zero, CursorMode.ForceSoftware); //Force software because otherwise it is hidden in Editor mode.
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void SetCursorToNormalArrow ()
    {
        Cursor.SetCursor(NormalArrow, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }
}