using UnityEngine;
using System.Collections;

public class DialogueSystemManager : MonoBehaviour
{
    [SerializeField] DialoguePanelsControl dialoguePanels;

    bool inDialogue = false;

    int dialogueIndex = 0;
    NarrativeEvent currentNarrativeEvent;

    float CanClickTimer;

    //Add a minor delay before the player can progress the dialogue.
    const float CanClickCooldown = 0.2f;

    void Start()
    {
        StartDialogue(1);
    }

    public void StartDialogue(int narrativeIndex)
    {
        inDialogue = true;
        dialogueIndex = 0;

        //currentNarrativeEvent = NarrativeEventLoader.GetSceneNarrative(1);
        dialoguePanels.ClearTextBoxes();
        ProgressDialogueSequence();
        StartCoroutine(ListForKeyPress());
    }

    IEnumerator ListForKeyPress()
    {
        while (inDialogue)
        {
            if (CanClickTimer > 0f)
            {
                CanClickTimer -= Time.deltaTime;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ProgressDialogueSequence();
                    CanClickTimer = CanClickCooldown;
                }
            }
            yield return null;
        }
    }

    void ProgressDialogueSequence()
    {
        if (dialogueIndex < currentNarrativeEvent.dialogues.Count)
        {
            dialoguePanels.DisplayDialoguePanel(currentNarrativeEvent.dialogues[dialogueIndex]);
            dialogueIndex++;
        }
        else
        {
            DialogueReachedTheEnd();
        }
    }

    void DialogueReachedTheEnd()
    {
        CanClickTimer = 0f;
        inDialogue = false;
        dialoguePanels.EndDialogue();
    }
}