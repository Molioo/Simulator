using Molioo.Simulator.Quests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIQuestPanelQuest : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _titleText = null;

    [SerializeField]
    private RectTransform _myRect = null;

    [SerializeField]
    private RectTransform _tasksRect = null;

    private List<UIQuestPanelTask> _createdTasks = new List<UIQuestPanelTask>();

    private QuestData _questData;

    public void SetQuest(QuestData questData,UIQuestPanelTask taskPrefab)
    {
        _questData = questData;
        
        _titleText.text = questData.Template.QuestName;
        foreach(QuestTaskData taskData in questData.Tasks)
        {
            UIQuestPanelTask task = Instantiate(taskPrefab,_tasksRect) as UIQuestPanelTask;
            task.SetQuestTask(taskData);
            _createdTasks.Add(task);
        }

        StartCoroutine(SetHeight());
    }

    public bool RepresentsQuest(QuestData questData)
    {
        return _questData.QuestID == questData.QuestID;
    }

    private IEnumerator SetHeight()
    {
        yield return new WaitForEndOfFrame();

        float height = 20 + _titleText.preferredHeight;
        foreach(UIQuestPanelTask task in _createdTasks)
        {
            height += task.PreferredHeight;
        }
        _myRect.sizeDelta = new Vector2(_myRect.sizeDelta.x, height);
    }
}
