using UnityEngine;
using System.Collections.Generic;
using System.Collections;


namespace MyNameSpace
{
    public class MotorState_Dead : MotorStateBase
    {
        public MotorState_Dead(PlayerController player, PlayerFeedbacks feedbacks) : base(player, feedbacks)
        {
            modules = new List<ModuleBase>()
        {
            new Module_Gravity(player, feedbacks),
            new Module_NoMovement(player, feedbacks),
        };
        }

        public override void StateEntry()
        {
            base.StateEntry();
            feedback.Animator.PlayDeath();
        }
        public override void TickUpdate()
        {
            base.TickUpdate();
        }

        protected override void Transitions()
        {
        }

        public override void StateExit()
        {
        }
    }
}