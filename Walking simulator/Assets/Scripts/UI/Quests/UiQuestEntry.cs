using Molioo.Simulator.Quests;
using System;
using TMPro;
using UnityEngine;

public class UiQuestEntry : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI _questDescriptionText;

    private QuestData _questData;

    private Action<UiQuestEntry> _onClickQuestEntry;

    public QuestData QuestData
    {
        get { return _questData; }
    }

    public void SetQuest(QuestData questData, Action<UiQuestEntry> onClickQuestEntry)
    {
        _questData = questData;
        _questDescriptionText.text = questData.Template.QuestDescription;
        _onClickQuestEntry += onClickQuestEntry;
    }

    public void OnClick()
    {
        if (_onClickQuestEntry != null)
            _onClickQuestEntry.Invoke(this);
    }

}
