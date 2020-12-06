using UnityEngine;
using System.Collections;

public static class Greetings 
{
    static string[] positive = new string[]
    {
        "Hi great friend!!",
        "So glad I get to see you again!",
        "I missed you!"
    };

    static string[] neutral = new string[]
    {
        "Hi stranger",
        "Oh.. didn't see you there",
        "We should talk to get to know each other"
    };

    static string[] negative = new string[]
    {
        "No... I was trying to avoid you",
        "You make me feel sick",
        "Talk to the hand"
    };

    public static string GetGreeting (ApprovalLevels approval)
    {
        switch (approval)
        {
            case ApprovalLevels.Dislike:    return GetPositiveGreeting;
            case ApprovalLevels.Neutral:    return GetNeutralGreeting;
            case ApprovalLevels.Like:    
            default:                        return GetNegativeGreeting;
        }
    }

    public static string GetPositiveGreeting => positive[Random.Range(0, positive.Length)];
    public static string GetNeutralGreeting => neutral[Random.Range(0, neutral.Length)];
    public static string GetNegativeGreeting => negative[Random.Range(0, negative.Length)];
}