using UnityEngine;

namespace Molioo.Simulator.Quests
{
    public class AddQuestHelper : MonoBehaviour
    {
        [SerializeField]
        private QuestTemplate _questToAdd;

        [SerializeField]
        private EQuestStatus _questStatusToSet;

        public void TryToAddQuest()
        {
            if(_questStatusToSet == EQuestStatus.Discovered)
            {
                QuestsManager.Instance.AddQuestAsReadyToStart(_questToAdd);
            }
            else if (_questStatusToSet == EQuestStatus.InProgress)
            {
                QuestsManager.Instance.SetQuestAsActive(_questToAdd);
            }    
        }
    }
}