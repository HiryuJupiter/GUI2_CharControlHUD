using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



namespace MyNameSpace
{
    /*
     The NPC's interaction is made up of several UI panels: 
    1. The greetings page
    2. The dialogue page
    3. The quest page
    4. The shop page
    Each page is operated by a seperate class
     */


    public class NPC : MonoBehaviour
    {
        static NPC NpcInPlayerFocus;
        static bool InConversation;

        [Header("Reference")]
        [SerializeField] GameObject playerInRangeIndicator;

        [Header("Properties")]
        [SerializeField] bool hasDialogue = true;
        [SerializeField] List<ItemID> sellingItems;
        [SerializeField] Quest quest;

        [Header("Dialogue")]
        [SerializeField] NPCDialogue dialogue;
        //[SerializeField] NPCDialogue dialogue;

        //Status
        ItemSaveFile[] itemList;
        Approval approval;

        //References
        DialogueInterfaceController dialogueInterface;
        QuestInterfaceController questInterface;
        GreetingsInterfaceController landingInterface;
        Inventory_Shop shopInterface;

        LayerMask playerBodyLayer;
        PlayerController player;

        //Properties
        public ConversationStages conversationStage { get; private set; } = ConversationStages.Closed;
        public bool HasDialogue => hasDialogue;
        public bool HasQuest => quest != null && quest.QuestName != "";
        public bool HasShop => sellingItems.Count > 0;
        public Quest Quest => quest;
        public ApprovalLevels ApprovalLevel => approval.level;
        public DialogueBranch GetDialogueBranch => dialogue.GetBranch(approval.level);
        public ItemSaveFile[] ItemList => itemList;

        #region MonoBehavior
        protected void Awake()
        {
            //Initialize
            approval = new Approval();

            playerBodyLayer = CharacterSettings.instance.PlayerBodyLayer;
            itemList = new ItemSaveFile[Inventory.NPCShopSlots];
            for (int i = 0; i < itemList.Length; i++)
            {
                if (sellingItems.Count > i && sellingItems[i] != ItemID.Empty)
                {
                    itemList[i] = new ItemSaveFile(sellingItems
                        [i], 1);
                }
            }
        }

        void Start()
        {
            //Reference
            player = PlayerController.Instance;

            landingInterface = GreetingsInterfaceController.Instance;
            dialogueInterface = DialogueInterfaceController.Instance;
            shopInterface = Inventory_Shop.Instance;
            questInterface = QuestInterfaceController.Instance;
        }
        #endregion

        #region Conversation interface transitions
        public void GoToConversationStage(ConversationStages newStage)
        {
            //Based on the conversation stage, turn off and on corresponding pages
            if (newStage != conversationStage)
            {
                conversationStage = newStage;

                switch (newStage)
                {
                    case ConversationStages.Initiation:
                        landingInterface.SetIsOpen(true);
                        dialogueInterface.SetIsOpen(false);
                        shopInterface.SetIsOpen(false);
                        questInterface.SetIsOpen(false);

                        landingInterface.Initialize(this);
                        break;
                    case ConversationStages.Dialogue:
                        landingInterface.SetIsOpen(false);
                        dialogueInterface.SetIsOpen(true);
                        shopInterface.SetIsOpen(false);
                        questInterface.SetIsOpen(false);

                        dialogueInterface.Initialiize(this);
                        break;
                    case ConversationStages.Shop:
                        shopInterface.NPCRequestOpenShop(this);

                        landingInterface.SetIsOpen(false);
                        dialogueInterface.SetIsOpen(false);
                        shopInterface.SetIsOpen(true);
                        questInterface.SetIsOpen(false);
                        break;
                    case ConversationStages.Quest:
                        landingInterface.SetIsOpen(false);
                        dialogueInterface.SetIsOpen(false);
                        shopInterface.SetIsOpen(false);
                        questInterface.SetIsOpen(true);

                        questInterface.Initialize(this);
                        break;
                    case ConversationStages.Closed:
                        landingInterface.SetIsOpen(false);
                        dialogueInterface.SetIsOpen(false);
                        shopInterface.SetIsOpen(false);
                        questInterface.SetIsOpen(false);
                        break;
                }
            }
        }

        public void EndConversation()
        {
            //When the conversation ends, we want to turn back on the "Press enter to interact" prompt
            //so that the player can keep talk to the NPC
            InConversation = false;
            GoToConversationStage(ConversationStages.Closed);
            if (NpcInPlayerFocus == this)
            {
                playerInRangeIndicator.SetActive(true);
                StartCoroutine(WaitForConversationInitiation());
            }
        }
        #endregion

        #region Conversation Initiation
        //When player enteres the NPC's proximity, start testing for engage input.
        void OnTriggerEnter(Collider other)
        {
            if (IsColliderPlayer(other))
            {
                if (NpcInPlayerFocus == null)
                {
                    NpcInPlayerFocus = this;
                    if (!InConversation)
                    {
                        playerInRangeIndicator.SetActive(true);
                        StartCoroutine(WaitForConversationInitiation());
                    }
                }
            }
        }

        //We use a coroutine to check for starting conversation as there could me many NPCs and this operation can slow things down.
        IEnumerator WaitForConversationInitiation()
        {
            while (NpcInPlayerFocus == this && !InConversation)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    InitiateConversation();
                }
                yield return null;
            }
        }

        void InitiateConversation()
        {
            InConversation = true;
            conversationStage = ConversationStages.Initiation;

            landingInterface.SetIsOpen(true);
            landingInterface.Initialize(this);
        }
        #endregion

        #region Conversation Closing
        void OnTriggerExit(Collider other)
        {
            if (IsColliderPlayer(other))
            {
                if (NpcInPlayerFocus == this)
                {
                    NpcInPlayerFocus = null;
                    playerInRangeIndicator.SetActive(false);
                    if (InConversation)
                    {
                        EndConversation();
                    }
                }
            }
        }
        #endregion

        public void ApplyApprovalAffect(ApprovalAffect affect) => approval.ApplyInfluence(affect);

        bool IsColliderPlayer(Collider col) => playerBodyLayer == (playerBodyLayer | 1 << col.gameObject.layer);


        void OnGUI()
        {
            if (GUI.Button(new Rect(600, 300, 200, 20), "Increase approval"))
            {
                approval.ApplyInfluence(true);
            }
            else if (GUI.Button(new Rect(800, 300, 200, 20), "Decrease approval"))
            {
                approval.ApplyInfluence(false);
            }
        }
    }
}