using UnityEngine;
using System.Collections;


namespace MyNameSpace
{
    public class Module_NoMovement : ModuleBase
    {
        float moveXSmoothDampVelocity;
        float moveZSmoothDampVelocity;

        //Ctor
        public Module_NoMovement(PlayerController motor, PlayerFeedbacks feedback) : base(motor, feedback)
        { }

        #region Public methods
        public override void ModuleEntry()
        {
            base.ModuleEntry();
        }

        public override void TickUpdate()
        {
            base.TickUpdate();
        }

        public override void TickFixedUpdate()
        {
            //Modify x-velocity
            status.currentVelocity.x = Mathf.SmoothDamp(status.currentVelocity.x, 0f, ref moveXSmoothDampVelocity, settings.SteerSpeedGround * Time.deltaTime);
            status.currentVelocity.z = Mathf.SmoothDamp(status.currentVelocity.z, 0f, ref moveZSmoothDampVelocity, settings.SteerSpeedGround * Time.deltaTime);
        }

        public override void ModuleExit()
        {
            base.ModuleExit();
        }
        #endregion
    }
}