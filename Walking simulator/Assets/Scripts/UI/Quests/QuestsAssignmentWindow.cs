using Molioo.Simulator.Quests;
using System.Collections.Generic;
using UnityEngine;

public class QuestsAssignmentWindow : MonoBehaviour
{

    [SerializeField]
    private RectTransform _availableQuestsRect = null;

    [SerializeField]
    private RectTransform _todaysQuestsRect = null;

    [SerializeField]
    private UiQuestEntry _questEntryPrefab = null;

    private List<UiQuestEntry> _availableQuestsEntries = new List<UiQuestEntry>();
    private List<UiQuestEntry> _todaysQuestsEntries = new List<UiQuestEntry>();

    private void OnEnable()
    {
        Refresh();
    }

    private void Refresh()
    {
        CleanLists();
        CreateAvailableQuests();
        CreateTodaysQuests();
    }

    public void OnClickQuestEntry(UiQuestEntry questEntry)
    {
        if(questEntry.QuestData.QuestStatus == EQuestStatus.ReadyToStart)
        {
            QuestsManager.Instance.SetQuestAsActive(questEntry.QuestData.Template);
            Refresh();
        }
    }


    private void CreateAvailableQuests()
    {
        foreach (QuestData quest in QuestsManager.Instance.GetQuestsWithStatus(EQuestStatus.ReadyToStart))
        {
            UiQuestEntry questEntry = Instantiate(_questEntryPrefab, _availableQuestsRect);
            questEntry.SetQuest(quest, OnClickQuestEntry);
            _availableQuestsEntries.Add(questEntry);
        }
    }

    private void CreateTodaysQuests()
    {
        foreach (QuestData quest in QuestsManager.Instance.GetQuestsWithStatus(EQuestStatus.InProgress))
        {
            UiQuestEntry questEntry = Instantiate(_questEntryPrefab, _todaysQuestsRect);
            questEntry.SetQuest(quest, OnClickQuestEntry);
            _todaysQuestsEntries.Add(questEntry);
        }
    }

    private void CleanLists()
    {
        for(int i= 0; i < _availableQuestsEntries.Count; i++)
        {
            Destroy(_availableQuestsEntries[i].gameObject);
        }

        for (int i = 0; i < _todaysQuestsEntries.Count; i++)
        {
            Destroy(_todaysQuestsEntries[i].gameObject);
        }

        _availableQuestsEntries.Clear();
        _todaysQuestsEntries.Clear();
    }

}
