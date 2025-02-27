﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;



namespace MyNameSpace
{
    public class MotorState_Hurt : MotorStateBase
    {
        public MotorState_Hurt(PlayerController player, PlayerFeedbacks feedbacks) : base(player, feedbacks)
        {
            modules = new List<ModuleBase>()
        {
            new Module_Gravity(player, feedbacks),
            new Module_HurtKnockBack(player, feedbacks),
        };
        }

        public override void StateEntry()
        {
            base.StateEntry();
            //feedback.Animator.PlayHurt();

            //Apply knockback
            //int directionalSign = (int)Mathf.Sign(motor.transform.position.x - motorStatus.lastEnemyPosition.x);
            //Vector2 knockBack = settings.HurtDirection;
            //knockBack.x *= directionalSign;
            //motorStatus.currentVelocity = knockBack;

            ////Exit hurt state after a certain duration
            //motor.StartCoroutine(ExitHurtState());
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
            //feedback.Animator.PlayHurt();
        }

        IEnumerator ExitHurtState()
        {
            yield return new WaitForSeconds(settings.HurtDuration);

            if (motorStatus.isOnGround)
            {
                player.SwitchToNewState(MotorStates.OnGround);
            }
            else
            {
                player.SwitchToNewState(MotorStates.Aerial);
            }
        }
    }
}