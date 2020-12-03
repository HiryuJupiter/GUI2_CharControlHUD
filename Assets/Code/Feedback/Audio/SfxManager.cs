using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager instance;

    [Header("Item")]
    [SerializeField] GameObject sfx_InventoryOpen;
    [SerializeField] GameObject sfx_PickedUpGold;
    public void SpawnUI_InventoryOpen() => SpawnSfxPrefab(sfx_InventoryOpen);
    public void SpawnUI_PickedUpGold() => SpawnSfxPrefab(sfx_PickedUpGold);

    [Header("UI")]
    [SerializeField] GameObject sfx_UI_1_Click;
    [SerializeField] GameObject sfx_UI_2_bom;
    [SerializeField] GameObject sfx_UI_3_tape;
    [SerializeField] GameObject sfx_UI_4_shake;
    public void SpawnUI_1_Click() => SpawnSfxPrefab(sfx_UI_1_Click);
    public void SpawnUI_2_bom() => SpawnSfxPrefab(sfx_UI_2_bom);
    public void SpawnUI_3_tape() => SpawnSfxPrefab(sfx_UI_3_tape);
    public void SpawnUI_4_shake() => SpawnSfxPrefab(sfx_UI_4_shake);

    [Header("Player")]
    [SerializeField] GameObject sfx_Char_Hurt;
    [SerializeField] GameObject sfx_Char_Respawn;
    public void Spawn_PlayerHurt() => SpawnSfxPrefab(sfx_Char_Hurt);
    public void Spawn_PlayerRespawn() => SpawnSfxPrefab(sfx_Char_Respawn);

    [Header("Enemy")]
    [SerializeField] GameObject sfx_EnemyHurt;
    public void Spawn_EnemyHurt() => SpawnSfxPrefab(sfx_EnemyHurt);

    [Header("NPC")]
    [SerializeField] GameObject sfx_NPCHowCanIHelp;
    public void Spawn_NpcHowCanIHelp() => SpawnSfxPrefab(sfx_NPCHowCanIHelp);

    [Header("Weapons")]
    [SerializeField] GameObject sfx_Swing1;
    [SerializeField] GameObject sfx_Swing2;
    [SerializeField] GameObject sfx_Whirl;
    public void Spawn_Swing1() => SpawnSfxPrefab(sfx_Swing1);
    public void Spawn_Swing2() => SpawnSfxPrefab(sfx_Swing2);
    public void Spawn_Whirl() => SpawnSfxPrefab(sfx_Whirl);

    #region MonoBehavior
    void Awake()
    {
        instance = this;
    }
    #endregion

    #region Private
    void SpawnSfxPrefab (GameObject pf)
    {
        Instantiate(pf, Vector3.zero, Quaternion.identity);
    }
    #endregion
}