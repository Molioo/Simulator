using System.Collections.Generic;
using UnityEngine;

namespace Molioo.Simulator.Quests
{
    public class QuestsManager : Singleton<QuestsManager>
    {
        [SerializeField]
        private List<Quest> _currentQuests = new List<Quest>();

        public void TryToCompleteTask(QuestTask questTask)
        {
            for (int i = 0; i < _currentQuests.Count; i++)
            {
                if (_currentQuests[i].TryToCompleteTask(questTask.QuestTaskId))
                    break;
            }
        }

        public void TryToCompleteTask(string questTaskID)
        {
            for (int i = 0; i < _currentQuests.Count; i++)
            {
                if (_currentQuests[i].TryToCompleteTask(questTaskID))
                    break;
            }
        }
    }
}