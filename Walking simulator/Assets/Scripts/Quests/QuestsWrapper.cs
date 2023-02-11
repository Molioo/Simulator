using System.Collections.Generic;
using UnityEngine;

namespace Molioo.Simulator.Quests
{
    public class QuestsWrapper
    {
        public List<QuestData> CurrentQuests;

        public QuestsWrapper(List<QuestData> currentQuests)
        {
            CurrentQuests = currentQuests;
        }
    }
}