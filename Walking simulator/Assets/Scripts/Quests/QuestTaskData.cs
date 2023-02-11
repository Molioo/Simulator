namespace Molioo.Simulator.Quests
{
    [System.Serializable]
    public class QuestTaskData
    {
        public string QuestTaskId = "";

        public bool TaskComplete = false;

        public int CurrentValue = 0;

        public int MaxValue = 1;

        public QuestTaskData(QuestTaskTemplate template)
        {
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
