using UnityEngine;
using System.Collections;



namespace MyNameSpace
{
    [DefaultExecutionOrder(-10000)]
    public class CharacterSettings : MonoBehaviour
    {
        public static CharacterSettings instance { get; set; }

        [Header("Layers")]
        [SerializeField] LayerMask groundLayer;
        [SerializeField] LayerMask enemyLayer;
        [SerializeField] LayerMask playerBodyLayer;
        [SerializeField] LayerMask interactableLayer;
        public LayerMask GroundLayer => groundLayer;
        public LayerMask EnemyLayer => enemyLayer;
        public LayerMask PlayerBodyLayer => playerBodyLayer;
        public LayerMask InteractableLayer => interactableLayer;

        [Header("Player Movement")]
        [SerializeField] float steerSpeedGround = 1f; //50f
        [SerializeField] float steerSpeedAir = 5f; //50f
        [SerializeField] float playeRunSpeed = 15;
        [SerializeField] float playerWalkSpeed = 10;
        [SerializeField] float playerCrouchSpeed = 6;
        public float SteerSpeedGround => steerSpeedGround;
        public float SteerSpeedAir => steerSpeedAir;
        public float PlayerRunSpeed => playeRunSpeed;
        public float PlayerMoveSpeed => playerWalkSpeed;
        public float PlayerCrawlSpeed => playerCrouchSpeed;

        [Header("Normal Jump")]
        [SerializeField] float minJumpForce = 12f;
        [SerializeField] float maxJumpForce = 22f;
        [SerializeField] float maxCoyoteDuration = 0.25f;
        public float MinJumpForce => minJumpForce;
        public float MaxJumpForce => maxJumpForce;
        public float MaxCoyoteDuration => maxCoyoteDuration;

        [Header("Hurt State")]
        [Range(10f, 50f)] [SerializeField] float hurtSlideSpeed = 20f; //50f

        [SerializeField] Vector3 hurtDirection = new Vector3(0f, 25f, 20f);
        [SerializeField] float hurtDuration = 0.5f;
        public float HurtSlideSpeed => hurtSlideSpeed;
        public Vector2 HurtDirection => hurtDirection;
        public float HurtDuration => hurtDuration;

        [Header("Gravity")]

        [SerializeField] float maxFallSpeed = -15f;
        [SerializeField] float gravity = 80f;
        public float MaxFallSpeed => maxFallSpeed;
        public float Gravity => gravity;

        [Header("Crouch")]
        [SerializeField] Vector2 crouchOffset;
        [SerializeField] Vector2 crouchSize;
        public Vector2 CrouchOffset => crouchOffset;
        public Vector2 CrouchSize => crouchSize;

        void Awake()
        {
            //Singleton
            if (instance == null)
            {
                instance = this;
            }
        }
    }
}