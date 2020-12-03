using UnityEngine;
using System.Collections;

public class PlayerControllerStatus
{
    const float QueueTimerMax = 0.5f;

    //Attck
    public float currentAttackAnimationDuration;
    public Ability nextAbility;
    public float channelingTimer;
    public float attackQueueTimer;
    public int nextAbilitySlotIndex;

    //Move
    public bool isOnGround;
    public bool isOnGroundPrevious;
    public bool isJumping;
    public Vector3 currentVelocity;

    //Jump
    public float coyoteTimer;
    public float jumpQueueTimer;

    //Hurt state
    public Vector2 lastEnemyPosition;

    //Convenience properties
    public bool isFalling => currentVelocity.y < 0f;
    public bool isMovingUp => currentVelocity.y > 0f;
    //public bool canJump => isOnGround || (coyoteTimer > 0f && !isJumping);
    public bool isInAttackAnimation => currentAttackAnimationDuration > 0f;
    public bool canJump => !isInAttackAnimation && (isOnGround || (coyoteTimer > 0f && !isJumping));
    public bool justLanded => !isOnGroundPrevious && isOnGround;

    public void CachePreviousStatus()
    {
        isOnGroundPrevious = isOnGround;
    }

    public void TickUpdate()
    {
        TickTimer();
    }

    public void QueueAttack (Ability ability)
    {
        nextAbility = ability;
        attackQueueTimer = QueueTimerMax;
    }

    public void ClearAttackQueue()
    {
        attackQueueTimer = -1f;
    }

    void TickTimer()
    {
        attackQueueTimer -= Time.deltaTime;
        currentAttackAnimationDuration -= Time.deltaTime;
        channelingTimer -= Time.deltaTime;
    }
}


/*
	 Here are 2 timers. Queue timer is longer than attack interval. Player can queue up an attack, but 
	 ...the script will have to wait for attack interval to expire before executing the queued attack.
	 
	  It is possible for the queue timer to expire when the player is stunned or dead or something.

Minimum attack interval: //Minimum interval between playing new animations, to prevent animations cancelling too soon
QueueTimerMax = //A "coyote timer" that lets the player queue up new attacks when it is not ready
	 */