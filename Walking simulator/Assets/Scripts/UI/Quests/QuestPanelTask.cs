using Molioo.Simulator.Quests;
using TMPro;
using UnityEngine;

public class QuestPanelTask : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _taskDescriptionText = null;

    [SerializeField]
    private RectTransform _taskDescriptionRect = null;

    private QuestTaskData _taskData;

    private void Update()
    {
        if(Time.frameCount % 10 == 0)
        {
            SetTaskText();
        }
    }

    public void SetQuestTask(QuestTaskData taskData)
    {
        _taskData = taskData;
        SetTaskText();
    }

    private void SetTaskText()
    {
        _taskDescriptionText.text = _taskData.Template.QuestTaskText + (_taskData.Template.ShowProgress ? " "+ _taskData.CurrentValue + "/" + _taskData.RequiredValue : "");
        _taskDescriptionRect.sizeDelta = new Vector2(_taskDescriptionRect.sizeDelta.x, _taskDescriptionText.preferredHeight + 10);
    }
}
