using Molioo.Simulator.Quests;
using System.Collections.Generic;
using UnityEngine;

public class QuestStatusRequirement : Requirement
{
    [SerializeField]
    private QuestTemplate _questToCheck;

    [SerializeField]
    private List<EQuestStatus> _possibleStatuses = new List<EQuestStatus>();

    public override bool IsRequirementMet()
    {
        bool hasStatusRequired = false;
        for (int i = 0; i < _possibleStatuses.Count; i++)
        {
            if (QuestsManager.Instance.HasQuestWithThatStatus(_questToCheck, _possibleStatuses[0]))
            {
                hasStatusRequired = true;
                break;
            }
        }
        return hasStatusRequired;
    }
}
