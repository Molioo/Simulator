using System.Collections.Generic;
using UnityEngine;

namespace Molioo.Simulator.Quests
{
    public class QuestsManager : Singleton<QuestsManager>
    {
        [SerializeField]
        private List<QuestTemplate> _allQuestsTemplates = new List<QuestTemplate>();

        [SerializeField]
        private List<QuestData> _currentQuests = new List<QuestData>();

        public void Start()
        {
            //Test
            SetQuestAsActive(_allQuestsTemplates[0]);
        }

        public void SetQuestAsActive(QuestTemplate template)
        {
            QuestData newQuest = new QuestData(template);
            //Check if there isn't one activated already in current quests
            _currentQuests.Add(newQuest);
        }

        public void TryToCompleteTask(QuestTaskTemplate questTask)
        {
            for (int i = 0; i < _currentQuests.Count; i++)
            {
                if (_currentQuests[i].TryToCompleteTask(questTask.QuestTaskId))
                    break;
            }
        }

        public void TryToAddProgressToTask(QuestTaskTemplate questTask,int progress)
        {
            for (int i = 0; i < _currentQuests.Count; i++)
            {
                if (_currentQuests[i].TryToAddProgressToTask(questTask.QuestTaskId, progress))
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