using UnityEngine;
using System.Collections;

public class StatPageOpener : CanvasPageBase
{
    [SerializeField] Camera equipmentCamera;

    StatPageStatsWriter statWriter;
    PlayerController player;

    void Start()
    {
        player = PlayerController.Instance;
        statWriter = StatPageStatsWriter.Instance;

        //UpdateAllPlayerInfo();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleOpen();
        }
    }

    public void UpdateAllPlayerInfo()
    {
        statWriter.InitializeAllInfo(player.data);
    }

    public override void SetIsOpen(bool isOpen)
    {
        base.SetIsOpen(isOpen);
        equipmentCamera.enabled = isOpen;
        if (isOpen)
        {
            UpdateAllPlayerInfo();
        }
    }
}