using UnityEngine;
using System.Collections;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public int requiredAmount;
    public int currentAmount;


    #region Quest Goal Checker
    public bool isReached()
    {
        return (this.currentAmount >= this.requiredAmount);
    }
    public bool isCompleted()
    {
        return (this.currentAmount == this.requiredAmount);
    }
    #endregion

    #region Quest Goals
    public void Destroyed()
    {
        if (goalType == GoalType.Destroy)
            currentAmount = GameManager.instance.thor.questCurrentCollectableAmount;
            requiredAmount = GameManager.instance.thor.questRequiredCollectableAmount;
            currentAmount--;
    }

    public void Captured()
    {
        if (goalType == GoalType.Capture)
            currentAmount = GameManager.instance.thor.questCurrentCollectableAmount;
            requiredAmount = GameManager.instance.thor.questRequiredCollectableAmount;
            currentAmount++;
    }

    public void Defended()
    {
        if (goalType == GoalType.Defend)
        {
            currentAmount = GameManager.instance.thor.questCurrentCollectableAmount;
            requiredAmount = GameManager.instance.thor.questRequiredCollectableAmount;
        }
    }

    public void Created()
    {
        if (goalType == GoalType.Create)
        {
            currentAmount = GameManager.instance.thor.questCurrentCollectableAmount;
            requiredAmount = GameManager.instance.thor.questRequiredCollectableAmount;
            currentAmount++;
        }
    }

    #endregion
}
public enum GoalType
{
    Destroy,
    Capture,
    Defend,
    Create
}
