using UnityEngine;
using System.Collections;


namespace MyNameSpace
{
    public abstract class ModuleBase
    {
        protected CharacterSettings settings;
        protected PlayerController player;
        protected PlayerControllerStatus status;
        protected PlayerFeedbacks feedback;
        protected MotorRaycaster raycaster;

        public ModuleBase(PlayerController player, PlayerFeedbacks feedback)
        {
            this.player = player;
            this.feedback = feedback;
            status = player.status;
            raycaster = player.Raycaster;

            settings = CharacterSettings.instance;
        }

        public virtual void ModuleEntry() { }
        public virtual void TickFixedUpdate() { }
        public virtual void TickUpdate() { }
        public virtual void ModuleExit() { }
    }
}