using UnityEngine;
using System.Collections;


public class Approval : MonoBehaviour
{
    public ApprovalLevels level = ApprovalLevels.Neutral;

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

    void Decrease ()
    {
        level = level == ApprovalLevels.Dislike ? ApprovalLevels.Neutral : ApprovalLevels.Like;
    }

    void Increase()
    {
        level = level == ApprovalLevels.Like ? ApprovalLevels.Neutral : ApprovalLevels.Dislike;
    }

}