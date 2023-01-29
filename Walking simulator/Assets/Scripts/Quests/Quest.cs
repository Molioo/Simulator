using System.Collections.Generic;
using UnityEngine;

namespace Molioo.Simulator.Quests
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Molioo/Quests/New Quest")]
    public class Quest : ScriptableObject
    {
        public string QuestID = "";

        [SerializeField]
        private List<QuestTask> _tasks = new List<QuestTask>();

        public bool HasTask(string taskID)
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].QuestTaskId == taskID)
                    return true;
            }
            return false;
        }

        public bool TryToCompleteTask(string taskID)
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].QuestTaskId == taskID)
                    _tasks[i].CompleteTask();
                return true;
            }

            return false;
        }
    }
}