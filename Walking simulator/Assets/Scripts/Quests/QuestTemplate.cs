using System.Collections.Generic;
using UnityEngine;

namespace Molioo.Simulator.Quests
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Molioo/Quests/New Quest")]
    public class QuestTemplate : ScriptableObject
    {
        public string QuestID = "";

        public string QuestName = "";

        public List<QuestTaskTemplate> TasksTemplates = new List<QuestTaskTemplate>();

        public QuestTaskTemplate GetQuestTaskTemplate(string taskID)
        {
            for (int i = 0; i < TasksTemplates.Count; i++)
            {
                if (TasksTemplates[i].QuestTaskId == taskID)
                    return TasksTemplates[i];
            }
            return null;
        }

    }
}