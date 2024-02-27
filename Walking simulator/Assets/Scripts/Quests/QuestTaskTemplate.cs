using UnityEngine;

namespace Molioo.Simulator.Quests
{
    [System.Serializable]
    public class QuestTaskTemplate
    {
        public string QuestTaskId = "";

        public string QuestTaskText = "";

        public bool ShowProgress = true;

        public int RequiredValue = 1;

        public string DependsOnTask = "";
    }
}