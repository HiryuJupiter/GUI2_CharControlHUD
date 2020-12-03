using UnityEngine;
using System.Collections;
/*
 mechanim is not appliable to frame based animation, because you can just tell it to 
Instead of setfloat, just animator.Play(

Using setFloat you're offloading logic to another system that handles the transition, 
but you're probably also handling logic handling in code, it's much easier to do everything in code in one place

coolhotkey: alt + arrow to shift lines


 */
public class Player3DAnimator : MonoBehaviour
{
    //Component reference
    Animator animator;
    int currentState;

    //Parameter ID - movements
    int jumpParamID;
    int idleParamID;
    int walkParamID;
    int deathParamID;

    //Parameter ID - attacks
    int slash1ParamID;
    int slash2ParamID;
    int whirlwindAttckParamID;
    int FireLobParamID;
    int IcyShieldParamID;



    bool inAttackAnimation;

    int slashSequenceIndex = 1;

    #region Mono
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        //Param ID: Booleans
        jumpParamID = Animator.StringToHash("Jump");
        idleParamID = Animator.StringToHash("Idle");
        walkParamID = Animator.StringToHash("Walk");
        deathParamID = Animator.StringToHash("Death");

        slash1ParamID = Animator.StringToHash("Slash1");
        slash2ParamID = Animator.StringToHash("Slash2");
        whirlwindAttckParamID = Animator.StringToHash("Whirl");
        FireLobParamID = Animator.StringToHash("FireLob");
        IcyShieldParamID = Animator.StringToHash("IcyShield");
    }
    #endregion

    #region Movement animations
    public void PlayIdle()
    {
        if (!inAttackAnimation)
        {
            ChangeAnimationState(idleParamID);
        }
    }
    public void PlayWalk() => ChangeAnimationState(walkParamID);
    public void PlayDeath() => ChangeAnimationState(deathParamID);
    #endregion

    #region Attack animations
    public void PlayAbilityAnimation (AbilityTypes ability)
    {
        switch (ability)
        {
            case AbilityTypes.Slash:
                PlaySlash();
                break;
            case AbilityTypes.Whirl:
                PlayWhirl();
                break;
        }
    }
    #endregion

    void ChangeAnimationState(int newState)
    {
        //Change to new animation state only if it's a different one
        if (currentState != newState)
        {
            if (newState == idleParamID)
            {
                animator.CrossFade(newState, 0.1f); //Smoothly go back to idle
            }
            else
            {
                //Instantly play animations to make it snappy and responsive
                animator.Play(newState);
            }
            currentState = newState;
        }
    }

    float GetCurrentAnimationDuration()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    IEnumerator DelayedTransitionToAnimation(float delay, int newAnimationParamID)
    {
        yield return new WaitForSeconds(delay);
        animator.Play(newAnimationParamID);
    }

    #region Attack animations
    void PlaySlash()
    {
        slashSequenceIndex = slashSequenceIndex == 2 ? 1 : 2;
        ChangeAnimationState(slashSequenceIndex == 2 ? slash2ParamID : slash1ParamID);
    }

    void PlayWhirl() => ChangeAnimationState(whirlwindAttckParamID);
    //public void PlayIcyBlast() => ChangeAnimationState(IcyShieldParamID);
    //public void PlayFireLob() => ChangeAnimationState(FireLobParamID);
    #endregion
}