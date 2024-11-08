using Core.Game.QuestManager.Data;

namespace Core.Game.QuestManager.Events
{
    public struct QuestTerminateEvent
    {
        public Quest quest { get; set; }

        public QuestTerminateEvent(Quest quest)
        {
            this.quest = quest;
        }
    }
}