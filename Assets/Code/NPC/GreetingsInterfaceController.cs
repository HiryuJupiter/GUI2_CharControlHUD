using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GreetingsInterfaceController : CanvasPageToggle
{
    public static GreetingsInterfaceController Instance;

    [SerializeField] Text bodyText;

    [SerializeField] GameObject btn_OpenDialogue;
    [SerializeField] GameObject btn_OpenQuest;
    [SerializeField] GameObject btn_OpenShop;

    NPC npc;

    void Awake() => Instance = this;


    public void Initialize (NPC npc)
    {
        this.npc = npc;
        bodyText.text = Greetings.GetGreeting(npc.ApprovalLevel);

        //Reveal the buttons based on what could happen with the NPC
        btn_OpenDialogue.SetActive(npc.HasDialogue);
        btn_OpenQuest.SetActive(npc.HasQuest);
        btn_OpenShop.SetActive(npc.HasShop);
    }

    public void SelectDialogue() => ExitToNewStage(ConversationStages.Dialogue);
    public void SelectShop() => ExitToNewStage(ConversationStages.Shop);
    public void SelectQuest() => ExitToNewStage(ConversationStages.Quest);

    //public override void SetIsOpen(bool isOpen)
    //{
    //    base.SetIsOpen(isOpen);
    //}

    void ExitToNewStage (ConversationStages newStage)
    {
        bodyText.text = "";
        npc.GoToConversationStage(newStage);
    }
}