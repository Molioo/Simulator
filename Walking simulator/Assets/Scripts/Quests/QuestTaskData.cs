using System;
using System.Diagnostics;

namespace Molioo.Simulator.Quests
{
    [Serializable]
    public class QuestTaskData
    {
        public string QuestTaskId = "";

        public bool TaskComplete = false;

        public int CurrentValue = 0;

        public int RequiredValue = 1;

        [NonSerialized]
        public QuestTaskTemplate Template;

        public QuestTaskData(QuestTaskTemplate template)
        {
            Template = template;
            QuestTaskId = template.QuestTaskId;
            CurrentValue = 0;
            RequiredValue = template.RequiredValue;
        }

        public void CompleteTask()
        {
            TaskComplete = true;
        }

        public void AddProgressToTask(int valueToAdd)
        {
            UnityEngine.Debug.Log("Add progress to task");
            CurrentValue += valueToAdd;
            if (CurrentValue >= RequiredValue)
            {
                CompleteTask();
            }
        }

    }
}
