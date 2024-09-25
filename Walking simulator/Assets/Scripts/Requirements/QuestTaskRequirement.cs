using Molioo.Simulator.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTaskRequirement : Requirement
{
    [SerializeField]
    private string _questToCheck;

    [SerializeField]
    private bool _requireTaskCompleted=false;

    public override bool IsRequirementMet()
    {
        return true;
        //bool hasStatusRequired = false;
        //{
        //    if (QuestsManager.Instance.HasQuestWithThatStatus(_questToCheck, _possibleStatuses[0]))
        //    {
        //        hasStatusRequired = true;
        //        break;
        //    }
        //return hasStatusRequired;
    }

}
