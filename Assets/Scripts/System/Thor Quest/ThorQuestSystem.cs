using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ThorQuestSystem : MonoBehaviour
{
    //Emulate Odin Event System
    public static ThorQuestSystem current;

    public int questCurrentCollectableAmount;
    public int questRequiredCollectableAmount;

    private void Awake()
    {
        current = this;
    }
}

[System.Serializable]
public class Quest
{
    public bool isActive;
    public string questTitle;
    public string questDesription;
    public int questExp;
    public int questScore;

    public QuestGoal goal;

    public void QuestComplete() {
        isActive = false;
        Debug.Log(questTitle + " was completed!");
    }
}
