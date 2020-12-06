using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialoguePanelAnimationController))]
public class DialoguePanelsControl : MonoBehaviour
{
    public static DialoguePanelsControl instance;

    [SerializeField] DialoguePanel rightPanel;
    [SerializeField] DialoguePanel leftPanel;

    DialoguePanelAnimationController animator;

    void Awake()
    {
        instance = this;
        animator = GetComponent<DialoguePanelAnimationController>();
    }

    public void ClearTextBoxes ()
    {
        leftPanel.Clear();
        rightPanel.Clear();
    }

    public void DisplayDialoguePanel (Dialogue dialogue)
    {
        //dialogue.position == 0 mean the character is on the left side.
        if (dialogue.position == 0) 
        {
            animator.SetLeftPanelOpen(true);
            leftPanel.DisplayDialogue(dialogue);
            rightPanel.GreyOutPanel();
        }
        else
        {
            animator.SetRightPanelOpen(true);
            rightPanel.DisplayDialogue(dialogue);
            leftPanel.GreyOutPanel();
        }
    }

    public void EndDialogue()
    {
        animator.SetLeftPanelOpen(false);
        animator.SetRightPanelOpen(false);
    }
}
