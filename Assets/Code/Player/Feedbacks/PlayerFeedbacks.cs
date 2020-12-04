using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Player3DAnimator))]
[RequireComponent(typeof(PlayerColorAssigner))]
[RequireComponent(typeof(Player3rdPersonCamera))]
[RequireComponent(typeof(PlayerController))]

public class PlayerFeedbacks : MonoBehaviour
{
    [SerializeField] Transform modelTransform;

    //Reference
    Player3rdPersonCamera cameraController;
    PlayerController player;

    //Status
    bool facingRight;
    Vector3 faceRightScale, faceLeftScale;

    public PlayerColorAssigner playerColorAssigner { get; set; }
    public Player3DAnimator Animator { get; set; }

    public Transform ModelTransform => modelTransform;

    void Awake()
    {
        Animator                = GetComponent<Player3DAnimator>();
        playerColorAssigner     = GetComponent<PlayerColorAssigner>();
        cameraController        = GetComponent<Player3rdPersonCamera>();

        //Cache the scales 
        faceRightScale = transform.localScale;
        faceLeftScale = faceRightScale;
        faceLeftScale.x *= -1f;
    }

    void Start()
    {
        player = PlayerController.Instance;
    }

    #region Facing
    public void SetFacingToFront()
    {
        SetFacing(Quaternion.LookRotation(cameraController.NonTiltedDirectionTowardsPlayer, Vector3.up));
    }

    public void SetFacingToTarget(Vector3 pos)
    {
        Vector3 untiltedDir = pos - transform.position;
        untiltedDir.y = 0f;

        SetFacing(Quaternion.LookRotation(untiltedDir, Vector3.up));
    }

    public void SetFacingBasedOnMovement()
    {
        Vector3 vel = player.status.currentVelocity;
        if (vel != Vector3.zero)
        {
            vel.y = 0f;
            SetFacing(Quaternion.LookRotation(cameraController.NonTiltedRotationTowardsPlayer * vel, Vector3.up));
        }
    }

    void SetFacing(Quaternion facing)
    {
        modelTransform.localRotation = facing;
    }
    #endregion

}