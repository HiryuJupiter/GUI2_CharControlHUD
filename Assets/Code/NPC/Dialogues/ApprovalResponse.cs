using UnityEngine;
using System.Collections;

//A small class that encapsulates the string response of choices
public class ApprovalResponse
{
    public string response_dislike = "I hate you";
    public string response_neutral = "I'm neutral towards you";
    public string response_like = "I like you";

    public string GetResponse (ApprovalLevels approval)
    {
        switch (approval)
        {
            case ApprovalLevels.Dislike:    return response_dislike;
            case ApprovalLevels.Neutral:    return response_neutral;
            case ApprovalLevels.Like: 
            default:                        return response_like;
                
        }
    }
}