using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(DeadScreen))]
[RequireComponent(typeof(AbilitiesHotbar))]

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    HUDManager hud;
    DeadScreen deadScreen;
    AbilitiesHotbar hotbar;
    StatPageOpener inventoryPage;


    #region MonoBehavior
    void Awake()
    {
        //Lazy singleton
        Instance = this;

        //Reference
        deadScreen      = GetComponent<DeadScreen>();

        hud             = GetComponentInChildren<HUDManager>();
        inventoryPage   = GetComponentInChildren<StatPageOpener>();
        hotbar          = GetComponentInChildren<AbilitiesHotbar>();
    }

    void Start()
    {
        
    }
    #endregion

    #region Public
    public void UpdateAllPlayerInfo (GameData data)
    {
        UpdateHealth(data.stats.HP, data.stats.HPMax);
        UpdateMana(data.stats.MP, data.stats.MPMax);
        UpdateAP(data.stats.AP, data.stats.APMax);
        hotbar.UpdateSlotIcons(data);
    }

    public void PlayerDead ()
    {
        deadScreen.SetIsOpen(true);
    }

    public void PlayerRespawned()
    {
        deadScreen.SetIsOpen(false);
    }
    #endregion

    #region HUD
    public void OnDamaged()
    {
        //When hurt, set red borders to visible
        hud.FlashDamageBorder();
    }

    public void UpdateHealth(int current, int max)
    {
        hud.SetHealthBar(current / (float)max);
    }

    public void UpdateMana(int current, int max)
    {
        hud.SetManaBar(current / (float)max);
    }

    public void UpdateAP(int current, int max)
    {
        hud.SetStaminaBar(current / (float)max);
    }
    #endregion

    #region Abilities Hotbar
    public void SetHotbarOnCoolDown (int slotIndex, float duration)
    {
        hotbar.DisplayCooldown(slotIndex, duration);
    }
    #endregion
}