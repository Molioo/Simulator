using UnityEngine;

namespace Molioo.Simulator.Quests
{
    [CreateAssetMenu(fileName = "New Quest Task", menuName = "Molioo/Quests/New Quest Task")]
    public class QuestTask : ScriptableObject
    {
        public string QuestTaskId = "";

        public string QuestTaskText = "";

        public bool TaskComplete = false;

        public void CompleteTask()
        {
            TaskComplete = true;
            Debug.Log("Task " + QuestTaskId + " completed");
        }
    }
}