using System;
using System.Collections.Generic;
using Core.Game.SceneManager.Events;
using UnityEngine;

namespace Core.UI.LoadingWidget.Handler
{
    public struct LoadingTask
    {
        public Dictionary<int, String> tasks { get; set; }
        public int numOfTotalTasks { get; set; }
        public int numOfCompleteTasks { get; set; }
        public bool isCompleted { get; set; }
    }

    public class LoadWidgetProgressHandler
    {
        public LoadWidgetView view { get; set; }
        public LoadingTask loadingTask { get; set; }

        public LoadWidgetProgressHandler(LoadWidgetView view)
        {
            this.view = view;
        }

        public void CreateLoadingTask(SceneRequestTransferEvent sceneRequestTransferEvent)
        {
            view.gameObject.SetActive(true);
            LoadingTask lt = new LoadingTask();
            int numOfTasks = 0;
            if (sceneRequestTransferEvent.unLoadedScene.Length != 0)
            {
                lt.tasks[numOfTasks] = sceneRequestTransferEvent.unLoadedScene;
                numOfTasks++;
            }

            if (sceneRequestTransferEvent.targetScene.Length != 0)
            {
                lt.tasks[numOfTasks] = sceneRequestTransferEvent.targetScene;
                numOfTasks++;
            }

            lt.numOfTotalTasks = numOfTasks;
            lt.numOfCompleteTasks = 0;
            lt.isCompleted = false;
            StartLoadingTask(lt);
        }

        public void StartLoadingTask(LoadingTask lt)
        {
            loadingTask = lt;
            view.loadingStages.SetText(loadingTask.numOfCompleteTasks + "0/" + loadingTask.numOfTotalTasks);
        }

        public void Update()
        {
            if (!loadingTask.isCompleted)
            {
                view.loadingStages.SetText(loadingTask.numOfCompleteTasks + "/" + loadingTask.numOfTotalTasks);
            }

            if (view.loadingScene != null && view.loadingScene.isDone == false)
            {
                float progress = Mathf.Clamp01(view.loadingScene.progress / 0.9f);
                view.loadingPercentage.SetText(progress * 100 + "%");
                if (view.loadingScene.progress >= 0.9f)
                {
                    progress = 1.0f;
                }

                view.loadingBar.localScale = new Vector3(progress, 1, 1);
            }

            if (view.unloadingScene != null && view.unloadingScene.isDone == false)
            {
                float progress = Mathf.Clamp01(view.unloadingScene.progress / 0.9f);
                view.loadingPercentage.SetText(progress * 100 + "%");
                if (view.unloadingScene.progress >= 0.9f)
                {
                    progress = 1.0f;
                }

                view.loadingBar.localScale = new Vector3(progress, 1, 1);
            }
        }

        public void Destroy()
        {
        }
    }
}