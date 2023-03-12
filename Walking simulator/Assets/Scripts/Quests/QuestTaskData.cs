using System;

namespace Molioo.Simulator.Quests
{
    [Serializable]
    public class QuestTaskData
    {
        public string QuestTaskId = "";

        public bool TaskComplete = false;

        public int CurrentValue = 0;

        public int RequiredValue = 1;

        public bool ShowProgress = true;

        public QuestTaskTemplate Template;

        public QuestTaskData(QuestTaskTemplate template)
        {
            Template = template;
            QuestTaskId = template.QuestTaskId;
            CurrentValue = 0;
            RequiredValue = template.RequiredValue;
            ShowProgress = template.ShowProgress;
        }

        public void CompleteTask()
        {
            TaskComplete = true;
        }

        public void AddProgressToTask(int valueToAdd)
        {
            CurrentValue += valueToAdd;
            if (CurrentValue >= RequiredValue)
            {
                CompleteTask();
            }
        }

    }
}
