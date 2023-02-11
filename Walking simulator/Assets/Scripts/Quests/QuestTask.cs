using UnityEngine;

namespace Molioo.Simulator.Quests
{
    [CreateAssetMenu(fileName = "New Quest Task", menuName = "Molioo/Quests/New Quest Task")]
    public class QuestTask : ScriptableObject
    {
        public string QuestTaskId = "";

        public string QuestTaskText = "";

        public bool TaskComplete = false;

        public int CurrentValue = 0;

        public int MaxValue = 1;

        public void CompleteTask()
        {
            TaskComplete = true;
        }

        public void AddProgressToTask(int valueToAdd)
        {
            CurrentValue += valueToAdd;
            if(CurrentValue >= MaxValue)
            {
                CompleteTask();
            }
        }
    }
}