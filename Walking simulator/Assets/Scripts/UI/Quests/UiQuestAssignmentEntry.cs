using Molioo.Simulator.Quests;
using System;
using TMPro;
using UnityEngine;

public class UiQuestAssignmentEntry : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI _questDescriptionText;

    private QuestData _questData;

    private Action<UiQuestAssignmentEntry> _onClickQuestEntry;

    public QuestData QuestData
    {
        get { return _questData; }
    }

    public void SetQuest(QuestData questData, Action<UiQuestAssignmentEntry> onClickQuestEntry)
    {
        _questData = questData;
        _questDescriptionText.text = questData.Template.QuestName;
        _onClickQuestEntry += onClickQuestEntry;
    }

    public void OnClick()
    {
        if (_onClickQuestEntry != null)
            _onClickQuestEntry.Invoke(this);
    }

}
