using UnityEngine;

namespace Molioo.Simulator.Quests
{
    public class CompleteTaskHelper : MonoBehaviour
    {
        [SerializeField]
        private QuestTask _questTask = null;

        public void CompleteTask()
        {
            if (_questTask != null)
            {
                QuestsManager.Instance.TryToCompleteTask(_questTask);
            }
        }
    }
}