using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueInterfaceController : CanvasPageToggle
{
    public static DialogueInterfaceController Instance { get; private set; }

    [SerializeField] Text bodyText;
    [SerializeField] GameObject buttonResponse1;
    [SerializeField] Text       buttonResponse1Text;
    [SerializeField] GameObject buttonResponse2;
    [SerializeField] Text       buttonResponse2Text;

    NPC npc;
    DialogueBranch branch;

    void Awake() => Instance = this;

    //Stage one, display dialogue and response options

    //Stage 2, show response buttons

    public void Initialiize(NPC npc)
    {
        this.npc = npc;
        DisplayBranch(npc.GetDialogueBranch);
    }

    void DisplayBranch (DialogueBranch branch)
    {
        this.branch = branch;

        bodyText.text = branch.Sentence;
        if (branch.Response1 != null && branch.Response1.ResponseText != "")
        {
            buttonResponse1.SetActive(true);
            buttonResponse1Text.text = branch.Response1.ResponseText;
        }

        if (branch.Response2 != null && branch.Response2.ResponseText != "")
        {
            buttonResponse2.SetActive(true);
            buttonResponse2Text.text = branch.Response2.ResponseText;
        }
    }

    public void ClickedResponse1 ()
    {
        if (npc != null && branch != null)
        {
            npc.ApplyApprovalAffect(branch.Response1.Affect);
            npc.EndConversation();
        }
    }

    public void ClickedResponse2()
    {
        if (npc != null && branch != null)
        {
            npc.ApplyApprovalAffect(branch.Response2.Affect);
            npc.EndConversation();
        }
    }
}

/*
     public IEnumerator DisplayGreeting(string[] lines)
    {
        int lineCount = lines.Length;
        int line = 0;

        bodyText.text = lines[line];

        while (isOpen && line < lineCount)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                if (++line < lineCount)
                {
                    bodyText.text = lines[line];
                }
                else
                {
                    npc.EndConversation();
                }
            }
            yield return null;
        }
    }
 */