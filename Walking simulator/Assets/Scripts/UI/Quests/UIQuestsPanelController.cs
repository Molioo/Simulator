using Molioo.Simulator.Quests;
using System.Collections.Generic;
using UnityEngine;

public class UIQuestsPanelController : MonoBehaviour
{
    [SerializeField]
    private UIQuestPanelQuest _questPanelEntryPrefab=null;

    [SerializeField]
    private UIQuestPanelTask _questPanelTaskPrefab = null;

    [SerializeField]
    private RectTransform _questsEntriesRect = null;

    private List<UIQuestPanelQuest> _createdQuests = new List<UIQuestPanelQuest>();


    public void Start()
    {
        QuestsManager.Instance.OnQuestStatusUpdate += OnQuestStatusUpdated;
    }

    public void OnDestroy()
    {
        QuestsManager.Instance.OnQuestStatusUpdate -= OnQuestStatusUpdated;
    }

    public void CreateQuests()
    {
        ClearAllCreatedQuests();
        foreach(QuestData questData in QuestsManager.Instance.GetQuestsWithStatus(EQuestStatus.InProgress))
        {
            CreateQuestAndAddToList(questData);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            CreateQuests();
        }
    }

    private void OnQuestStatusUpdated(QuestData questData)
    {
        Debug.Log("Received OnQuestStatusUpdated for Quest " + questData.QuestID + " with status " + questData.QuestStatus);

        if(questData.QuestStatus == EQuestStatus.InProgress)
        {
            if (IsQuestCreated(questData) == false)
            {
                CreateQuestAndAddToList(questData);
            }
            else
            {
                RefreshQuest(questData);
            }

        }
        
        if(questData.QuestStatus == EQuestStatus.Completed)
        {
            RemoveQuest(questData);
        }
    }

    private void CreateQuestAndAddToList(QuestData questData)
    {
        UIQuestPanelQuest questEntry = Instantiate(_questPanelEntryPrefab, _questsEntriesRect);
        questEntry.SetQuest(questData, _questPanelTaskPrefab);
        _createdQuests.Add(questEntry);
    }

    private void RefreshQuest(QuestData questData)
    {
        foreach (UIQuestPanelQuest quest in _createdQuests)
        {
            if (quest.RepresentsQuest(questData))
            {
                quest.RefreshTasks();
                break;
            }
        }
    }

    private void RemoveQuest(QuestData questData)
    {
        UIQuestPanelQuest foundQuest = null;

        foreach (UIQuestPanelQuest quest in _createdQuests)
        {
            if(quest.RepresentsQuest(questData))
            {
                foundQuest = quest;
                break;
            }
        }

        if (foundQuest == null)
        {
            return;
        }

        _createdQuests.Remove(foundQuest);
        Destroy(foundQuest.gameObject);
    }

    private void ClearAllCreatedQuests()
    {
        foreach (UIQuestPanelQuest quest in _createdQuests)
        {
            Destroy(quest.gameObject);
        }
        _createdQuests.Clear();
    }

    private bool IsQuestCreated(QuestData questData)
    {
        foreach (UIQuestPanelQuest quest in _createdQuests)
        {
            if (quest.RepresentsQuest(questData))
            {
                return true;
            }
        }

        return false;
    }
}
