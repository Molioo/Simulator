using System;

namespace Molioo.Simulator.Quests
{
    [Serializable]
    public class QuestTaskData
    {
        public string QuestTaskId = "";

        public bool TaskComplete = false;

        public int CurrentValue = 0;

        public int MaxValue = 1;

        //[NonSerialized]
        public QuestTaskTemplate Template;

        public QuestTaskData(QuestTaskTemplate template)
        {
            Template = template;
            QuestTaskId = template.QuestTaskId;
            CurrentValue = 0;
            MaxValue = template.MaxValue;
        }

        public void CompleteTask()
        {
            TaskComplete = true;
        }

        public void AddProgressToTask(int valueToAdd)
        {
            CurrentValue += valueToAdd;
            if (CurrentValue >= MaxValue)
            {
                CompleteTask();
            }
        }

    }
}
