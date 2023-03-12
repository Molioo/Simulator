using UnityEngine;

namespace Molioo.Simulator.Quests
{
    public class CompleteTaskHelper : MonoBehaviour
    {
        [SerializeField]
        private string _questTaskId;

        public void CompleteTask()
        {
            if (!string.IsNullOrEmpty(_questTaskId))
            {
                QuestsManager.Instance.TryToCompleteTask(_questTaskId);
            }
        }
    }
}