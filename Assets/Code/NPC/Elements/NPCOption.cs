using UnityEngine;
using System.Collections;

public class NPCOption
{
    public string buttonText { get; private set; }
    public ApprovalAffect approvalAffect { get; private set; }

    public NPCOption(string text, ApprovalAffect approvalAffect = ApprovalAffect.None)
    {
        this.buttonText = text;
        this.approvalAffect = approvalAffect;
    }
}