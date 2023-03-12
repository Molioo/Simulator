using Molioo.Simulator.Quests;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestPanelEntry : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _titleText = null;

    [SerializeField]
    private RectTransform _tasksRect = null;

    private List<QuestPanelTask> _createdTasks = new List<QuestPanelTask>();

    private QuestData _questData;

    public void SetQuest(QuestData questData,QuestPanelTask taskPrefab)
    {
        _questData = questData;
        _titleText.text = questData.Template.QuestName;
        foreach(QuestTaskData taskData in questData.Tasks)
        {
            QuestPanelTask task = Instantiate(taskPrefab,_tasksRect) as QuestPanelTask;
            task.SetQuestTask(taskData);
            _createdTasks.Add(task);
        }
    }
}
