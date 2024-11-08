using System;
using System.Collections;
using Core.Game.QuestManager.Data;
using Core.Game.QuestManager.Events;
using UnityEngine;

namespace Core.Game.QuestManager.Handler
{
    public class QuestProcessHandler
    {
        QuestManagerView view;

        private void InitQuest(Quest quest)
        {
            quest.InitQuest(view);
        }

        public void TerminateQuest(Quest quest)
        {
            quest.TerminateQuest();
        }

        public QuestProcessHandler(QuestManagerView view)
        {
            this.view = view;
            QuestManagerEvent.QuestTerminateEvent += OnQuestTerminateEvent;
            QuestManagerEvent.QuestLoadCompleteEvent += OnQuestLoadCompleteEvent;
            QuestManagerEvent.QuestCompleteEvent += OnQuestCompleteEvent;
        }

        private void OnQuestTerminateEvent(QuestTerminateEvent questTerminateEvent)
        {
            StartNextQuest(questTerminateEvent.quest);
        }

        private void OnQuestCompleteEvent(QuestCompleteEvent questCompleteEvent)
        {
            StartNextQuest(questCompleteEvent.quest);
        }

        private void StartNextQuest(Quest quest)
        {
            int questIndex = view.activeQuests.IndexOf(quest);
            if (questIndex >= 0 && questIndex < view.activeQuests.Count - 1)
            {
                InitQuest(view.activeQuests[questIndex + 1]);
            }
            else
            {
                return;
            }
        }
        
        private void OnQuestLoadCompleteEvent(QuestLoadCompleteEvent questLoadCompleteEvent)
        {
            bool automaticallyActiveQuest =
                questLoadCompleteEvent.questManagerView.sceneMetaView.isAutomaticallyActiveQuest;
            if (automaticallyActiveQuest && view.activeQuests[0] != null)
            {
                // We first init the quest, the quest object itself will 
                // automatically start business after InitQuest Completed
                InitQuest(view.activeQuests[0]);
            }
        }

        public void Destroy()
        {
            QuestManagerEvent.QuestTerminateEvent -= OnQuestTerminateEvent;
            QuestManagerEvent.QuestLoadCompleteEvent -= OnQuestLoadCompleteEvent;
            QuestManagerEvent.QuestCompleteEvent -= OnQuestCompleteEvent;
        }
    }
}