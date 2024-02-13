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

        // [NonSerialized]
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

        public bool TryToCompleteTask(string taskID)
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                if (Tasks[i].QuestTaskId == taskID)
                {
                    Tasks[i].CompleteTask();
                    return true;
                }
            }

            return false;
        }

        public bool TryToAddProgressToTask(string taskID, int progressToAdd)
        {
            UnityEngine.Debug.Log("Checking for task " + taskID + " in " + QuestID);
            for (int i = 0; i < Tasks.Count; i++)
            {
                UnityEngine.Debug.Log("QuestTaskId is " + Tasks[i].QuestTaskId + " and taskId is " + taskID);
                if (Tasks[i].QuestTaskId == taskID)
                {
                    Tasks[i].AddProgressToTask(progressToAdd);
                    return true;
                }
            }

            return false;
        }

    }
}