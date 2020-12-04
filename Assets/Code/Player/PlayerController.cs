using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[DefaultExecutionOrder(-101)]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MotorRaycaster))]
[RequireComponent(typeof(Player3rdPersonCamera))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    //Class and components

    Player3rdPersonCamera cameraController;
    PersistentGameData gameData;
    PlayerResourceManagement resourceManagement;
    PlayerAbilityDirectory directory;
    UIManager ui;

    SlotManager_Equipment inventory;

    //States
    MotorStates currentStateType;
    MotorStateBase currentStateClass;
    Dictionary<MotorStates, MotorStateBase> stateClassLookup;

    //Cache
    LayerMask layerInteractable;

    public PlayerControllerStatus status { get; set; }
    public GameData data { get; set; }
    public MotorRaycaster Raycaster { get; set; }
    public PlayerFeedbacks feedback { get; set; }
    public Rigidbody Rb { get; set; }

    #region MonoBehiavor
    void Awake()
    {
        //Reference
        Rb                  = GetComponent<Rigidbody>();
        Raycaster           = GetComponent<MotorRaycaster>();
        feedback            = GetComponentInChildren<PlayerFeedbacks>();
        cameraController    = GetComponent<Player3rdPersonCamera>();

        //Initialize
        Instance = this;

        status = new PlayerControllerStatus();
        stateClassLookup = new Dictionary<MotorStates, MotorStateBase>
        {
            {MotorStates.OnGround,  new MotorState_MoveOnGround(this, feedback)},
            {MotorStates.Aerial,    new MotorState_Aerial(this, feedback)},
            {MotorStates.Dead,      new MotorState_Dead(this, feedback) },
            //{MotorStates.Hurt,      new MotorState_Hurt(this, Feedbacks)},
        };

        currentStateType = MotorStates.OnGround;
        currentStateClass = stateClassLookup[currentStateType];
    }

    void Start()
    {
        //Static references
        gameData = PersistentGameData.Instance;
        ui = UIManager.Instance;
        directory = PlayerAbilityDirectory.Instance;
        inventory = SlotManager_Equipment.Instance;

        //Reference game data and use it to initialize the game
        data = gameData.SaveFile;

        //UI
        ui.UpdateAllPlayerInfo(data);

        //Model color
        feedback.playerColorAssigner.UpdateColor(gameData.SaveFile);

        //Initialize class
        resourceManagement = new PlayerResourceManagement(this);

        //Cache
        layerInteractable = CharacterSettings.instance.InteractableLayer;
    }
    
    void Update()
    {
        currentStateClass?.TickUpdate();
        status.TickUpdate();
        resourceManagement.PassiveRegen();
    }

    void FixedUpdate()
    {
        status.CachePreviousStatus();
        CalculateCurrentStatus();

        currentStateClass?.TickFixedUpdate();

        ExecuteRigidbodyVelocity();
    }

    void OnTriggerEnter(Collider col)
    {
        if (layerInteractable == (layerInteractable | 1 << col.gameObject.layer))
        {
            Debug.Log(" Player hits an interactable item: " + col.name);
            Item item = col.GetComponent<Item>();
            if (item != null)
            {
                if (inventory.TryPickUpItem(item))
                {
                    Destroy(col.gameObject);
                }
            }
        }
    }
    #endregion

    #region Public  - resources
    public void DamagePlayer(Vector2 enemyPos, int damage)
    {
        resourceManagement.DamagePlayer(damage);
    }

    public void HealPlayer(int heal)
    {
        resourceManagement.HealPlayer(heal);
    }

    public void FullyRestorePlayer()
    {
        resourceManagement.FullyHealPlayer();
    }

    public void LevelUp()
    {
        PlayerAttribute attr = gameData.SaveFile.attributes;
        gameData.SaveFile.stats.HP += (int)(attr.Endurance * 0.2f);
        gameData.SaveFile.stats.MP += (int)(attr.Willpower * 0.2f);
        gameData.SaveFile.stats.AP += (int)(attr.Agility * 0.2f);
    }
    #endregion

    #region Item
    public void SpawnPickupItem (Item item)
    {
        //Instantiate(item, transform.position, Quaternion.identity);
    }
    #endregion

    #region Abilities
    public void TryToQueueAbility(int index)
    {
        AbilityTypes abilityType = data.abilityLoadout.abilities[index];

        //Get Ability
        if (directory.TryGetAbility(abilityType, out Ability ability))
        {
            status.QueueAttack(ability);
            status.nextAbilitySlotIndex = index;
        }
        else
        {
            Debug.LogError("This shouldn't happen.");
        }
    }
    #endregion

    #region State machine
    public void SwitchToNewState(MotorStates newStateType)
    {
        if (currentStateType != newStateType)
        {
            currentStateType = newStateType;

            currentStateClass.StateExit();
            currentStateClass = stateClassLookup[newStateType];
            currentStateClass.StateEntry();
        }
    }

    public void PlayerDead()
    {
        SwitchToNewState(MotorStates.Dead);
        ui.PlayerDead();
    }

    public void RespawnPlayer()
    {
        transform.position = EnvironmentlPositions.Instance.RespawnPoint.position;
        ui.PlayerRespawned();
        SwitchToNewState(MotorStates.OnGround);
        FullyRestorePlayer();
    }
    #endregion

    #region Private
    void ExecuteRigidbodyVelocity()
    {
        //rb.velocity = transform.TransformDirection(status.currentVelocity);
        //rb.velocity = camera.TransformDirection(status.currentVelocity);

        Rb.velocity = cameraController.NonTiltedRotationTowardsPlayer * status.currentVelocity;
    }
    #endregion

    #region Pre-calculations
    void CalculateCurrentStatus()
    {
        status.isOnGround = Raycaster.OnGrounDcheck();
    }
    #endregion

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 500, 20), "Current State: " + currentStateType);

        GUI.Label(new Rect(20, 60, 290, 20), "=== GROUND MOVE === ");
        GUI.Label(new Rect(20, 80, 290, 20), "OnGround: " + status.isOnGround);
        GUI.Label(new Rect(20, 100, 290, 20), "onGroundPrevious: " + status.isOnGroundPrevious);
        GUI.Label(new Rect(20, 120, 290, 20), "GameInput.MoveX: " + GameInput.MoveX);
        GUI.Label(new Rect(20, 180, 290, 20), "currentVelocity: " + status.currentVelocity);


        GUI.Label(new Rect(200, 0, 290, 20), "=== JUMPING === ");
        GUI.Label(new Rect(200, 20, 290, 20), "coyoteTimer: " + status.coyoteTimer);
        GUI.Label(new Rect(200, 40, 290, 20), "jumpQueueTimer: " + status.jumpQueueTimer);
        GUI.Label(new Rect(200, 60, 290, 20), "GameInput.JumpBtnDown: " + GameInput.JumpBtnDown);
        GUI.Label(new Rect(200, 80, 290, 20), "jumping: " + status.isJumping);

        GUI.Label(new Rect(400, 0, 290, 20), "=== INPUT === ");
        GUI.Label(new Rect(400, 20, 290, 20), "MoveX: " + GameInput.MoveX);
        GUI.Label(new Rect(400, 40, 290, 20), "MoveZ: " + GameInput.MoveZ);

        GUI.Label(new Rect(600, 0, 290, 20), "=== CURRENT ABILITY === ");
        GUI.Label(new Rect(600, 20, 290, 20), "nextAbility: " + status.nextAbility);
        GUI.Label(new Rect(600, 40, 290, 20), "attackQueueTimer: " + status.attackQueueTimer);
        GUI.Label(new Rect(600, 60, 290, 20), "loseControlTimer: " + status.channelingTimer);
        //GUI.Label(new Rect(600, 80, 290, 20), "is cd ready: " + status.nextAbility.IsCooldownReady);
        //GUI.Label(new Rect(600, 100, 290, 20), "is cd ready: " + status.nextAbility.Cooldown);

        GUI.Label(new Rect(800, 0, 290, 20), "=== CURRENT ABILITY === ");
        GUI.Label(new Rect(800, 20, 290, 20), "ability 1: " + gameData.SaveFile.abilityLoadout.abilities[0]);
        GUI.Label(new Rect(800, 40, 290, 20), "ability 2: " + gameData.SaveFile.abilityLoadout.abilities[1]);
        GUI.Label(new Rect(800, 60, 290, 20), "ability 3: " + gameData.SaveFile.abilityLoadout.abilities[2]);

        //GUI.Label(new Rect(300, 120,		290, 20), "testLocation: " + testLocation);
    }
}


/*
     public void SetFacingToFront ()
    {
        feedback.SetFacing(Quaternion.LookRotation(cameraController.NonTiltedDirectionTowardsPlayer, Vector3.up));
    }
 */