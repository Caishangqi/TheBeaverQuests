using Core.Game.QuestManager.Data;
using Core.Game.QuestManager.Events;
using Core.Game.SceneManager;
using Core.Game.SceneManager.Events;
using Core.Game.SceneManager.SceneMeta;
using UnityEngine;

namespace Core.Game.QuestManager.Handler
{
    public class QuestLoadHandler
    {
        public QuestManagerView view { get; set; }


        public QuestLoadHandler(QuestManagerView view)
        {
            this.view = view;
            SceneEvent.SceneLoadCompleteEvent += OnSceneLoadCompleteEvent;
            SceneEvent.SceneUnLoadCompleteEvent += OnSceneUnLoadCompleteEvent;
            SceneEvent.SceneRequestUnLoadEvent += OnSceneRequestUnLoadEvent;
        }

        private void OnSceneRequestUnLoadEvent(SceneRequestUnLoadEvent obj)
        {
            //RemoveMissionFromQuestManager();
        }


        private void OnSceneUnLoadCompleteEvent(SceneUnLoadCompleteEvent sceneUnLoadCompleteEvent)
        {
            RemoveMissionFromQuestManager();
        }

        private void OnSceneLoadCompleteEvent(SceneLoadCompleteEvent sceneLoadCompleteEvent)
        {
            view.sceneMetaView = (SceneMetaView)Object.FindObjectOfType(typeof(SceneMetaView));
            if (view.sceneMetaView)
            {
                Debug.Log("[Quest]      Find SceneMetaView and inject into QuestLoadHandler");
                AddMissionFromSceneMeta();
            }
        }

        private void RemoveMissionFromQuestManager()
        {
            foreach (Quest quest in view.activeQuests)
            {
                if (quest.questState == EQuestState.IN_PROGRESS)
                {
                    quest.TerminateQuest();
                }
            }

            view.activeQuests.Clear();
            Debug.Log("[Quest]      Remove all Mission From QuestManager");
        }

        private void AddMissionFromSceneMeta()
        {
            Debug.Log("[Quest]      Adding Mission From SceneMeta...");
            foreach (Quest sceneQuest in view.sceneMetaView.sceneQuests)
            {
                AddMission(sceneQuest);
            }

            Debug.Log("[Quest]      Finishing Adding Mission From SceneMeta");
            // Finish adding all mission
            QuestManagerEvent.QuestLoadCompleteEvent.Invoke(new QuestLoadCompleteEvent(view));
        }

        public void AddMission(Quest quest)
        {
            Debug.Log("[Quest]      Adding Mission ->" + quest.questName);
            view.activeQuests.Add(quest);
        }

        public void CompleteMission(Quest quest)
        {
            if (view.activeQuests.Contains(quest))
            {
                quest.CompleteQuest();
                view.activeQuests.Remove(quest);
            }
        }

        public void Destroy()
        {
            SceneEvent.SceneLoadCompleteEvent -= OnSceneLoadCompleteEvent;
            SceneEvent.SceneUnLoadCompleteEvent -= OnSceneUnLoadCompleteEvent;
        }
    }
}