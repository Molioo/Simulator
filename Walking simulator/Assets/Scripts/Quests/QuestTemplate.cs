using System.Collections.Generic;
using UnityEngine;

namespace Molioo.Simulator.Quests
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Molioo/Quests/New Quest")]
    public class QuestTemplate : ScriptableObject
    {
        public string QuestID = "";

        public List<QuestTaskTemplate> Tasks = new List<QuestTaskTemplate>();

    }
}