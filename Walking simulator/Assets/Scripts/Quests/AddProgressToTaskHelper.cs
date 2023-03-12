using UnityEngine;

namespace Molioo.Simulator.Quests
{
    public class AddProgressToTaskHelper : MonoBehaviour
    {
        [SerializeField]
        private string _questTaskId = "";

        [SerializeField]
        private int _valueToAdd = 1;

        public void AddProgressToTask()
        {
            if (!string.IsNullOrEmpty(_questTaskId))
            {
                QuestsManager.Instance.TryToAddProgressToTask(_questTaskId, _valueToAdd);
            }
        }
    }
}