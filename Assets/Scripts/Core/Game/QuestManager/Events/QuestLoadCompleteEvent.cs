using System.Collections.Generic;
using Core.Game.QuestManager.Data;

namespace Core.Game.QuestManager.Events
{
    public struct QuestLoadCompleteEvent
    {
        public QuestManagerView questManagerView { get; set; }

        public QuestLoadCompleteEvent(QuestManagerView questManagerView) 
        {
            this.questManagerView = questManagerView;
        }
        
    }
}