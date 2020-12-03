using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialoguePanelAnimationController : MonoBehaviour
{
    public static DialoguePanelAnimationController instance;

    [SerializeField] Animator leftAnimator;
    [SerializeField] Animator rightAnimator;

    int openParamID;
    int closeParamID;

    bool leftAlreadyOpened = false;
    bool rightAlreadyOpened = false;

    private void Awake()
    {
        openParamID = Animator.StringToHash("Open");
        closeParamID = Animator.StringToHash("Close");
    }

    public void SetLeftPanelOpen (bool openLeft)
    {
        if (openLeft && !leftAlreadyOpened)
        {
            leftAnimator.Play(openParamID);
            leftAlreadyOpened = true;
        }
        else if (!openLeft && leftAlreadyOpened)
        {
            leftAnimator.Play(closeParamID);
            leftAlreadyOpened = false;
        }
    }

    public void SetRightPanelOpen(bool openRight)
    {
        if (openRight && !rightAlreadyOpened)
        {
            rightAnimator.Play(openParamID);
            rightAlreadyOpened = true;
        }
        else if (!openRight && rightAlreadyOpened)
        {
            rightAnimator.Play(closeParamID);
            rightAlreadyOpened = false;
        }
    }
}
