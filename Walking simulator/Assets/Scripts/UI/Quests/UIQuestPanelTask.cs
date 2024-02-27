using Molioo.Simulator.Quests;
using TMPro;
using UnityEngine;

public class UIQuestPanelTask : MonoBehaviour
{
    const string STRIKE_START = "<s>";
    const string STRIKE_END = "</s>";

    [SerializeField]
    private TextMeshProUGUI _taskDescriptionText = null;

    [SerializeField]
    private RectTransform _taskDescriptionRect = null;

    public float PreferredHeight
    {
        get => _taskDescriptionText.preferredHeight;
    }

    public EQuestTaskStatus TaskStatus => _taskData.TaskStatus;

    private QuestTaskData _taskData;

    private void Update()
    {
        if(Time.frameCount % 60 == 0)
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
        //What an abomination I created here
        string text = _taskData.TaskStatus == EQuestTaskStatus.Completed ? STRIKE_START : "";
        text += _taskData.Template.QuestTaskText + (_taskData.Template.ShowProgress ? " " + _taskData.CurrentValue + "/" + _taskData.RequiredValue : "");
        text += _taskData.TaskStatus == EQuestTaskStatus.Completed ? STRIKE_END : "";
        _taskDescriptionText.text = text;

        _taskDescriptionRect.sizeDelta = new Vector2(_taskDescriptionRect.sizeDelta.x, _taskDescriptionText.preferredHeight + 10);
    }
}
