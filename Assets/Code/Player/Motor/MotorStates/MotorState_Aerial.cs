using UnityEngine;
using System.Collections.Generic;


namespace MyNameSpace
{
    public class MotorState_Aerial : MotorStateBase
    {
        public MotorState_Aerial(PlayerController player, PlayerFeedbacks feedbacks) : base(player, feedbacks)
        {
            modules = new List<ModuleBase>()
        {
            new Module_Gravity(player, feedbacks),
            new Module_MoveInAir(player, feedbacks),
            new Module_Jump(player, feedbacks),
            new Module_Attack(player, feedback),
        };
        }

        public override void StateEntry()
        {
            base.StateEntry();
        }

        public override void TickUpdate()
        {
            base.TickUpdate();
        }

        protected override void Transitions()
        {
            if (motorStatus.isOnGround && (!motorStatus.isMovingUp || !motorStatus.isJumping))
            {
                player.SwitchToNewState(MotorStates.OnGround);
            }
        }
    }
}