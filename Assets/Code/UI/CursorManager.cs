using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace MyNameSpace
{
    public class CursorManager : MonoBehaviour
    {
        public static CursorManager Instance;
        static EventSystem eventSystem;

        public Texture2D NormalArrow;
        public Texture2D Crosshair;
        public static bool IsMouseOverUI { get; set; }
        Vector2 crossHairCenter;

        void Awake()
        {
            Instance = this;
            eventSystem = EventSystem.current;
            crossHairCenter = new Vector2(Crosshair.width / 2f, Crosshair.height / 2f);
        }

        void Update()
        {
            //Checks if we are mover over UI element
            IsMouseOverUI = eventSystem.IsPointerOverGameObject();
        }

        public void SetCursorToCrosshair()
        {
            Cursor.SetCursor(Crosshair, crossHairCenter, CursorMode.ForceSoftware); //Force software because otherwise it is hidden in Editor mode.
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void SetCursorToNormalArrow()
        {
            Cursor.SetCursor(NormalArrow, Vector2.zero, CursorMode.Auto);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }
}