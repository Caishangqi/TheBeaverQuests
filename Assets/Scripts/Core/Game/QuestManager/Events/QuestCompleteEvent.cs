using System;
using Core.Game.QuestManager.Data;

namespace Core.Game.QuestManager.Events
{
    public struct QuestCompleteEvent
    {
        public Quest quest { get; set; }

        public QuestCompleteEvent(Quest quest)
        {
            this.quest = quest;
        }
    }
}