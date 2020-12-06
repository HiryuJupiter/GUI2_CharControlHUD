using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class Quest
{
    [SerializeField] int levelRequirement ;

    [SerializeField] string questName;
    [SerializeField] string description;

    [SerializeField] ItemID[] itemRewards;
    [SerializeField] ItemID[] itemDemands;

    public int LevelRequirement => levelRequirement;

    public string QuestName => questName;
    public string Description => description;

    public ItemID[] ItemRewards => itemRewards;
    public ItemID[] ItemDemands => itemDemands;
    public QuestStatus Status { get; private set; } = QuestStatus.WaitingToAccept;

    public void AcceptQuest ()
    {
        Status = QuestStatus.InProgress;
        PlayerController.Instance.TakeQuest(this);
    }

    public void HandInItems()
    {
        Status = QuestStatus.HandedIn_RewardsAvailable;
        PlayerController.Instance.CompletedQuest(this);
    }

    public void CollectedRewards()
    {
        Status = QuestStatus.RewardCollected_AllDone;
    }
}