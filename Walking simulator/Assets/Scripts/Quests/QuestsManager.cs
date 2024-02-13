using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Molioo.Simulator.Quests
{
    public class QuestsManager : Singleton<QuestsManager>
    {
        public Action<QuestData> OnQuestStatusUpdate;

        [SerializeField]
        private List<QuestTemplate> _allQuestsTemplates = new List<QuestTemplate>();

        [SerializeField]
        private List<QuestData> _currentQuests = new List<QuestData>();

        private IEnumerator Start()
        {
            //Test
            yield return new WaitForSeconds(2);
            SetQuestAsActive(_allQuestsTemplates[0]);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                SaveCurrentQuests();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                LoadCurrentQuests();
            }
        }

        public void SaveCurrentQuests()
        {
            QuestsSaveSystem.SaveQuestsProgress(_currentQuests);
        }

        public void LoadCurrentQuests()
        {
            _currentQuests = QuestsSaveSystem.LoadCurrentQuests();
        }

        public void SetQuestAsActive(QuestTemplate template)
        {
            if (!IsQuestInList(template, _currentQuests))
            {
                QuestData newQuest = new QuestData(template)
                {
                    QuestStatus = EQuestStatus.InProgress
                };

                _currentQuests.Add(newQuest);
                OnQuestStatusUpdate?.Invoke(newQuest);
            }
            else
            {
                StartQuest(template);
            }
        }

        public void AddQuestAsReadyToStart(QuestTemplate template)
        {
            if (!IsQuestInList(template, _currentQuests))
            {
                QuestData newQuest = new QuestData(template)
                {
                    QuestStatus = EQuestStatus.Discovered
                };

                _currentQuests.Add(newQuest);
                OnQuestStatusUpdate?.Invoke(newQuest);
            }
        }

        public void StartQuest(QuestTemplate questTemplate)
        {
            for (int i = 0; i < _currentQuests.Count; i++)
            {
                if (_currentQuests[i].QuestID == questTemplate.QuestID)
                {
                    _currentQuests[i].QuestStatus = EQuestStatus.InProgress;
                    OnQuestStatusUpdate?.Invoke(_currentQuests[i]);
                }
            }
        }

        public bool HasQuestWithThatStatus(QuestTemplate questTemplate,EQuestStatus questStatus)
        {
            for (int i = 0; i < _currentQuests.Count; i++)
            {
                if (_currentQuests[i].QuestID == questTemplate.QuestID)
                {
                    return _currentQuests[i].QuestStatus == questStatus;
                }
            }
            return false;
        }

        public void TryToAddProgressToTask(string questTaskID, int progress)
        {
            for (int i = 0; i < _currentQuests.Count; i++)
            {
                if (_currentQuests[i].TryToAddProgressToTask(questTaskID, progress))
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

        public List<QuestData> GetQuestsWithStatus(EQuestStatus status)
        {
            List<QuestData> quests = new List<QuestData>();
            for (int i = 0; i < _currentQuests.Count; i++)
            {
                if (_currentQuests[i].QuestStatus == status)
                    quests.Add(_currentQuests[i]);
            }
            return quests;
        }

        private bool IsQuestInList(QuestTemplate questTemplate, List<QuestData> questsList)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                if (questsList[i].QuestID == questTemplate.QuestID)
                    return true;
            }
            return false;
        }

    }
}