using Molioo.Simulator.Quests;
using System.Collections.Generic;
using UnityEngine;

public class QuestsPanelController : MonoBehaviour
{
    [SerializeField]
    private QuestPanelEntry _questPanelEntryPrefab=null;

    [SerializeField]
    private QuestPanelTask _questPanelTaskPrefab = null;

    [SerializeField]
    private RectTransform _questsEntriesRect = null;

    private List<QuestPanelEntry> _createdQuests = new List<QuestPanelEntry>();

    public void CreateQuests()
    {
        foreach(QuestData questData in QuestsManager.Instance.GetQuestsWithStatus(EQuestStatus.InProgress))
        {
            QuestPanelEntry questEntry = Instantiate(_questPanelEntryPrefab, _questsEntriesRect) as QuestPanelEntry;
            questEntry.SetQuest(questData, _questPanelTaskPrefab);
            _createdQuests.Add(questEntry);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            CreateQuests();
        }
    }
}
