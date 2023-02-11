using UnityEngine;

namespace Molioo.Simulator.Quests
{
    public class AddProgressToTaskHelper : MonoBehaviour
    {
        [SerializeField]
        private QuestTaskTemplate _questTask = null;

        [SerializeField]
        private int _valueToAdd = 1;

        public void AddProgressToTask()
        {
            if (_questTask != null)
            {
                QuestsManager.Instance.TryToAddProgressToTask(_questTask, _valueToAdd);
            }
        }
    }
}