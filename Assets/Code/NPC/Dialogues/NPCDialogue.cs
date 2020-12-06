using UnityEngine;

[System.Serializable]
public class NPCDialogue
{
    [SerializeField] DialogueBranch likedBranch;
    [SerializeField] DialogueBranch neutralBranch;
    [SerializeField] DialogueBranch dislikedBranch;

    public DialogueBranch GetBranch(ApprovalLevels approval)
    {
        switch (approval)
        {
            case ApprovalLevels.Dislike: return likedBranch;
            case ApprovalLevels.Neutral: return neutralBranch;
            case ApprovalLevels.Like:
            default: return dislikedBranch;
        }
    }
}

[System.Serializable]
public class DialogueBranch
{
    [SerializeField] string sentence = "I'm talking about something";
    [SerializeField] Response response1;
    [SerializeField] Response response2;

    public string Sentence => sentence;
    public Response Response1 => response1;
    public Response Response2 => response2;
}

[System.Serializable]
public class Response
{
    [SerializeField] string responseText = "Do it";
    [SerializeField] ApprovalAffect affect = ApprovalAffect.None;

    public string ResponseText => responseText;
    public ApprovalAffect Affect => affect;
}

//Dialogue tree, branch, leaf
//Leaf = 1 line

/*
 
[System.Serializable]
public class NPCDialogue
{
    [SerializeField] string[] likedResponse = new string[]
    {
        "You know, you're a great person (Liked)",
        "Let me tell you a secret!",
        "In the north, there is a cave, inside are treasures!"
    };

    [SerializeField] string[] neutralResponse = new string[]
    {
        "We need to get to know each other more (netural)"
    };


    [SerializeField] string[] dislikedResponse = new string[] 
    { 
        "Sorry I'm busy doing something (dislike)"
    };

    public string[] GetSentence(ApprovalLevels approval)
    {
        switch (approval)
        {
            case ApprovalLevels.Dislike: return likedResponse;
            case ApprovalLevels.Neutral: return neutralResponse;
            case ApprovalLevels.Like:
            default: return dislikedResponse;
        }
    }
}

 */