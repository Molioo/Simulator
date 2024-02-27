using System;
using System.Collections.Generic;

namespace Molioo.Simulator.Quests
{
    [Serializable]
    public class QuestData
    {
        public string QuestID;

        public EQuestStatus QuestStatus;

        public List<QuestTaskData> Tasks = new List<QuestTaskData>();

        public QuestTemplate Template;

        public QuestData(QuestTemplate template)
        {
            Template = template;
            QuestID = template.QuestID;
            Tasks = new List<QuestTaskData>();
            foreach (QuestTaskTemplate taskTemplate in template.TasksTemplates)
            {
                Tasks.Add(new QuestTaskData(taskTemplate));
            }
            RefreshTasksStatuses();
        }

        public void RefreshTaskTemplates()
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                Tasks[i].Template = Template.GetQuestTaskTemplate(Tasks[i].QuestTaskId);
            }
        }

        public void RefreshTasksStatuses()
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                if (Tasks[i].TaskComplete)
                {
                    continue;
                }

                if (Tasks[i].Template.DependsOnTask == "")
                {
                    Tasks[i].TaskStatus = EQuestTaskStatus.Shown;
                    continue;
                }

                if (GetTaskStatus(Tasks[i].Template.DependsOnTask) == EQuestTaskStatus.Completed)
                {
                    Tasks[i].TaskStatus = EQuestTaskStatus.Shown;
                }
                else
                {
                    Tasks[i].TaskStatus = EQuestTaskStatus.Hidden;
                }
            }
        }

        public bool HasTask(string taskID)
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                if (Tasks[i].QuestTaskId == taskID)
                    return true;
            }
            return false;
        }

        public EQuestTaskStatus GetTaskStatus(string taskID)
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                if (Tasks[i].QuestTaskId == taskID)
                    return Tasks[i].TaskStatus;
            }
            return EQuestTaskStatus.Hidden;
        }

        public bool TryToCompleteTask(string taskID)
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                if (Tasks[i].QuestTaskId == taskID)
                {
                    Tasks[i].CompleteTask();
                    RefreshTasksStatuses();
                    return true;
                }
            }

            return false;
        }

        public bool TryToAddProgressToTask(string taskID, int progressToAdd)
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                if (Tasks[i].QuestTaskId == taskID)
                {
                    Tasks[i].AddProgressToTask(progressToAdd);
                    if (Tasks[i].TaskComplete)
                    {
                        RefreshTasksStatuses();
                    }
                    return true;
                }
            }

            return false;
        }

    }
}