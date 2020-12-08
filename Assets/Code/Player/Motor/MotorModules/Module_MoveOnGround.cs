using UnityEngine;
using System.Collections;


namespace MyNameSpace
{
    public class Module_MoveOnGround : ModuleBase
    {
        const float RotationSpeed = 2f;

        float moveXSmoothDampVelocity;
        float moveZSmoothDampVelocity;
        bool crawling;
        bool running;
        float movespeedMod;

        float moveSpeed => crawling ? settings.PlayerCrawlSpeed : (running ? settings.PlayerRunSpeed : settings.PlayerMoveSpeed);

        //Ctor
        public Module_MoveOnGround(PlayerController motor, PlayerFeedbacks feedback) : base(motor, feedback)
        { }

        #region Public methods
        public override void ModuleEntry()
        {
            base.ModuleEntry();
            AnimationUpdate();
            movespeedMod = player.data.stats.MoveSpeed / 50f;
        }

        public override void TickUpdate()
        {
            base.TickUpdate();

            crawling = Input.GetKey(KeyCode.LeftControl);
            running = Input.GetKey(KeyCode.LeftShift);

            //CharacterRotationUpdate();
            AnimationUpdate();
        }

        public override void TickFixedUpdate()
        {
            //Modify x-velocity
            status.currentVelocity.x = Mathf.SmoothDamp(status.currentVelocity.x, GameInput.MoveX * moveSpeed + movespeedMod, ref moveXSmoothDampVelocity, settings.SteerSpeedGround * Time.deltaTime);
            status.currentVelocity.z = Mathf.SmoothDamp(status.currentVelocity.z, GameInput.MoveZ * moveSpeed + movespeedMod, ref moveZSmoothDampVelocity, settings.SteerSpeedGround * Time.deltaTime);
        }

        public override void ModuleExit()
        {
            base.ModuleExit();
            crawling = false;
        }
        #endregion

        //void CharacterRotationUpdate ()
        //{
        //    float mouseX = GameInput.MoveX;

        //    player.RotateCharacter(GameInput.MoveX * RotationSpeed);
        //    //motor.RotateCharacter1(Input.GetAxis("Mouse X"));
        //}

        void AnimationUpdate()
        {
            if (!status.isInAttackAnimation)
            {
                if (GameInput.IsMoving)
                {
                    feedback.Animator.PlayWalk();
                }
                else
                {
                    feedback.Animator.PlayIdle();
                }
            }

            if (GameInput.IsMoving)
                feedback.SetFacingBasedOnMovement();
        }
    }
}