using UnityEngine;

namespace Molioo.Simulator.Quests
{
    [CreateAssetMenu(fileName = "New Quest Task", menuName = "Molioo/Quests/New Quest Task")]
    public class QuestTaskTemplate : ScriptableObject
    {
        public string QuestTaskId = "";

        public string QuestTaskText = "";

        public bool TaskComplete = false;

        public int MaxValue = 1;

    }
}