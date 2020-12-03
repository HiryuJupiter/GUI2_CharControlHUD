using UnityEngine;
using System.Collections;

public class Module_MoveInAir : ModuleBase
{
    const float RotationSpeed = 1f;
    public Module_MoveInAir(PlayerController motor, PlayerFeedbacks feedback) : base(motor, feedback)
    { }

    float moveXSmoothDampVelocity;
    float moveZSmoothDampVelocity;

    public override void TickUpdate()
    {
        AnimationUpdate();
        base.TickUpdate();
    }

    public override void TickFixedUpdate()
    {
        //Move
        //motorStatus.currentVelocity.x = Mathf.SmoothDamp(motorStatus.currentVelocity.x, GameInput.MoveX * settings.PlayerMoveSpeed, ref moveXSmoothDampVelocity, settings.SteerSpeedAir * Time.deltaTime);

        status.currentVelocity.x = Mathf.SmoothDamp(status.currentVelocity.x, GameInput.MoveX * settings.PlayerMoveSpeed, ref moveXSmoothDampVelocity, settings.SteerSpeedAir * Time.deltaTime);
        status.currentVelocity.z = Mathf.SmoothDamp(status.currentVelocity.z, GameInput.MoveZ * settings.PlayerMoveSpeed, ref moveZSmoothDampVelocity, settings.SteerSpeedAir * Time.deltaTime);
    }

    //void RotateCharacterWithMouse()
    //{
    //    float mouseX = GameInput.MoveX;

    //    player.RotateCharacter(GameInput.MoveX * RotationSpeed);
    //    //motor.RotateCharacter1(Input.GetAxis("Mouse X"));
    //}

    void AnimationUpdate()
    {
        if (GameInput.IsMoving)
        {
            feedback.SetFacingBasedOnMovement();
        }
    }
}