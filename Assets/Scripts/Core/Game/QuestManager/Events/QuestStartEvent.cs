using Core.Game.QuestManager.Data;

namespace Core.Game.QuestManager.Events
{
    public struct QuestStartEvent
    {
        public Quest quest { get; set; }

        public QuestStartEvent(Quest quest)
        {
            this.quest = quest;
        }
    }
}