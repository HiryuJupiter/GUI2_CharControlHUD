using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestInterfaceController : CanvasPageToggle
{
    public static QuestInterfaceController Instance { get; private set; }

    [SerializeField] Text nameText;
    [SerializeField] Text descriptionText;
    [SerializeField] GameObject buttonStartQuest;
    [SerializeField] GameObject buttonHandin;
    [SerializeField] GameObject buttonCollect;
    [SerializeField] GameObject questionCompleted;
    [SerializeField] Image[] rewardImages;

    NPC npc;
    PlayerController player;

    void Awake() => Instance = this;

    void Start()
    {
        player = PlayerController.Instance;
    }

    //NPC tells this script, the QuestInterfaceController, to reveal the quest page by the NPC
    public void Initialize(NPC npc)
    {
        this.npc = npc;
        nameText.text = npc.Quest.QuestName;
        descriptionText.text = npc.Quest.Description;

        //Display the reward images
        for (int i = 0; i < rewardImages.Length; i++)
        {
            if (i < npc.Quest.ItemRewards.Length)
            {
                rewardImages[i].enabled = true;
                rewardImages[i].sprite = ItemDirectory.GetItem(npc.Quest.ItemRewards[i]).icon;
            }
            else
            {
                rewardImages[i].enabled = false;
            }
        }

        //Display and hide buttons based on status
        OnlyRevealNecessaryButtons();
    }

    public void AcceptQuest()
    {
        if (npc != null && player.data.level >= npc.Quest.LevelRequirement)
        {
            npc.Quest.AcceptQuest();
            OnlyRevealNecessaryButtons();
        }
    }

    public void HandInItems()
    {
        if (npc != null && Inventory_Bag.Instance.TryRemoveItems(npc.Quest.ItemDemands))
        {
            npc.Quest.HandInItems();
        }
        OnlyRevealNecessaryButtons();
    }

    public void CollectedRewards()
    {
        npc.Quest.CollectedRewards();

        //Hide all the reward images!
        for (int i = 0; i < rewardImages.Length; i++)
        {
            rewardImages[i].enabled = false;
        }

        OnlyRevealNecessaryButtons();

        //Go through the rewards, and give the player the reward if they have inventory space, otherwise drop the reward on the ground
        foreach (var itemID in npc.Quest.ItemRewards)
        {
            if (!player.Inventory.TryPickUpItem(ItemDirectory.GetItem(itemID)))
            {
                ItemDirectory.GetItem(itemID).SpawnItem(player.transform.position);
            }
        }
    }

    void OnlyRevealNecessaryButtons()
    {
        if (npc == null) return;

        switch (npc.Quest.Status)
        {
            case QuestStatus.WaitingToAccept:
                buttonStartQuest.SetActive(true);
                buttonCollect.SetActive(false);
                buttonHandin.SetActive(false);
                questionCompleted.SetActive(false);
                break;
            case QuestStatus.InProgress:
                buttonStartQuest.SetActive(false);
                buttonHandin.SetActive(true);
                buttonCollect.SetActive(false);
                questionCompleted.SetActive(false);
                break;
            case QuestStatus.HandedIn_RewardsAvailable:
                buttonStartQuest.SetActive(false);
                buttonHandin.SetActive(false);
                buttonCollect.SetActive(true);
                questionCompleted.SetActive(false);
                break;
            case QuestStatus.RewardCollected_AllDone:
                buttonStartQuest.SetActive(false);
                buttonCollect.SetActive(false);
                buttonHandin.SetActive(false);
                questionCompleted.SetActive(true);
                break;
        }
    }

    public override void SetIsOpen(bool isOpen)
    {
        base.SetIsOpen(isOpen);
    }
}