using UnityEngine;
using System.Collections;



//Encapsulates the approval rating of an NPC
public class Approval : MonoBehaviour
{
    public ApprovalLevels level = ApprovalLevels.Neutral;

    //Method overloads so that we can modify the rating using both a bool and using an enum
    public void ApplyInfluence (bool isPositive)
    {
        if (isPositive)
        {
            Decrease();
        }
        else
        {
            Increase();
        }
    }

    public void ApplyInfluence(ApprovalAffect affect)
    {
        if (affect == ApprovalAffect.Increase)
        {
            Increase();
        }
        else if (affect == ApprovalAffect.Decrease)
        {
            Decrease();
        }
    }

    //Decrease and increase the levels accordingly
    void Decrease ()
    {
        level = level == ApprovalLevels.Dislike ? ApprovalLevels.Neutral : ApprovalLevels.Like;
    }

    void Increase()
    {
        level = level == ApprovalLevels.Like ? ApprovalLevels.Neutral : ApprovalLevels.Dislike;
    }

}