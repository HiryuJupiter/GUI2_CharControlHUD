using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace MyNameSpace
{
    public class Module_Attack : ModuleBase
    {
        const float SlashAnimationDuration = 0.25f;
        const float WhirlAnimationDuration = 0.34f;

        Transform model;

        public Module_Attack(PlayerController player, PlayerFeedbacks feedback) : base(player, feedback)
        {
            model = player.feedback.ModelTransform;
            status = player.status;
        }

        public override void TickUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                player.TryToQueueAbility(0);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                player.TryToQueueAbility(1);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                player.TryToQueueAbility(2);
            }

            CheckIfCanPlayQueuedAttack();
        }

        void CheckIfCanPlayQueuedAttack()
        {
            if (hasAQueuedUpAttack && playerNotChanneling && status.nextAbility.IsCooldownReady)
            //if (hasAQueuedUpAttack && playerNotChanneling)
            {
                ExecuteAbility(status.nextAbility);
                status.ClearAttackQueue();
            }
        }

        public void ExecuteAbility(Ability ability)
        {
            //Shoot projectile
            GameObject.Instantiate(ability.SpawnBullet, player.transform.position, model.rotation);
            GameObject.Instantiate(ability.Sfx, player.transform.position, model.rotation);

            //Set timer
            status.currentAttackAnimationDuration = ability.AnimationLength;
            status.channelingTimer = ability.ChannelingDuration;
            player.StartCoroutine(ability.GoOnCooldown());

            //Feedback
            //player.feedback.PlayAbilityAnimation(ability.abilityType);
            player.feedback.Animator.PlayAbilityAnimation(ability.AbilityType);

            //UI
            UIManager.Instance.SetHotbarOnCoolDown(status.nextAbilitySlotIndex, status.nextAbility.Cooldown);
        }

        bool hasAQueuedUpAttack => status.attackQueueTimer > 0f;
        bool playerNotChanneling => status.channelingTimer < 0f;
    }


    // || (Input.GetMouseButtonDown(0) && !CursorManager.IsMouseOverUI))
}