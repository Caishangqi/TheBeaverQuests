using System;
using System.Collections;
using Core.Character;
using Core.Character.Events;
using Core.Entity.Portal;
using Core.Entity.Portal.Events;
using Core.Game.SceneManager.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Game.SceneManager.Handler
{
    public class SceneHandler
    {
        public SceneManagerView view;
        public String loadedScene { get; private set; }

        IEnumerator OnLoadSceneAndWait(PortalRequestSceneLoadEvent portalRequestSceneLoadEvent)
        {
            var gameObjectScene = portalRequestSceneLoadEvent.portalView.gameObject.scene;
            // 卸载当前场景
            var unloadSceneAsync = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(gameObjectScene);
            yield return new WaitUntil(() => unloadSceneAsync.isDone);

            // 加载新场景
            var loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(
                portalRequestSceneLoadEvent.sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
            yield return new WaitUntil(() => loadSceneAsync.isDone);

            // 场景加载完成后的操作
            SceneEvent.SceneLoadCompleteEvent?.Invoke(new SceneLoadCompleteEvent(loadSceneAsync,
                portalRequestSceneLoadEvent.sceneName));
        }

        public IEnumerator LoadSceneAndWait(string sceneName)
        {
            // 加载新场景
            view.loadingScene =
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName,
                    UnityEngine.SceneManagement.LoadSceneMode.Additive);
            SceneEvent.SceneLoadingEvent?.Invoke(new SceneLoadingEvent(view.loadingScene, sceneName));
            yield return new WaitUntil(() => view.loadingScene.isDone);
            SceneEvent.SceneLoadCompleteEvent?.Invoke(new SceneLoadCompleteEvent(view.loadingScene, sceneName));
        }

        public IEnumerator UnLoadSceneAndWait(string sceneName)
        {
            Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
            if (scene.isLoaded)
            {
                view.unloadingScene = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
            }

            SceneEvent.SceneUnloadingEvent?.Invoke(new SceneUnloadingEvent(view.unloadingScene, sceneName));
            yield return new WaitUntil(() => view.unloadingScene.isDone);
            SceneEvent.SceneUnLoadCompleteEvent?.Invoke(new SceneUnLoadCompleteEvent(view.unloadingScene, sceneName));
        }


        public SceneHandler(SceneManagerView view)
        {
            this.view = view;
            PortalEvent.portalRequestSceneLoadEvent += OnPortalRequestSceneLoad;
            PlayerEvent.PlayerSetLocationEvent += OnPlayerSetLocation;
            SceneEvent.SceneRequestUnLoadEvent += OnSceneRequestUnLoadEvent;
            SceneEvent.SceneRequestLoadEvent += OnSceneRequestLoadEvent;
            SceneEvent.SceneRequestTransferEvent += OnSceneRequestTransferEvent;
            SceneEvent.SceneLoadCompleteEvent += OnSceneLoadCompleteEvent;
            SceneEvent.SceneUnLoadCompleteEvent += OnSceneUnLoadCompleteEvent;
        }

        private void OnSceneUnLoadCompleteEvent(SceneUnLoadCompleteEvent obj)
        {
            loadedScene = null;
        }

        private void OnSceneLoadCompleteEvent(SceneLoadCompleteEvent sceneLoadCompleteEvent)
        {
            loadedScene = sceneLoadCompleteEvent.sceneName;
        }

        /// <summary>
        /// A Complete Scene Transfer event, including unload scene, load scene,
        /// and trigger LoadingWidget. User are expected to invoke this event if
        /// they want to execute any scene to scene operation
        /// </summary>
        /// <param name="sceneRequestTransferEvent">the sceneRequestTransferEvent</param>
        private void OnSceneRequestTransferEvent(SceneRequestTransferEvent sceneRequestTransferEvent)
        {
            if (sceneRequestTransferEvent.unLoadedScene != "")
            {
                view.UnloadScene(sceneRequestTransferEvent.unLoadedScene);
            }

            if (sceneRequestTransferEvent.targetScene != "")
            {
                view.LoadScene(sceneRequestTransferEvent.targetScene);
            }
        }

        private void OnSceneRequestLoadEvent(SceneRequestLoadEvent sceneRequestLoadEvent)
        {
            view.LoadScene(sceneRequestLoadEvent.sceneName);
        }

        private void OnSceneRequestUnLoadEvent(SceneRequestUnLoadEvent sceneRequestUnLoadEvent)
        {
            view.UnloadScene(sceneRequestUnLoadEvent.sceneName);
        }

        public void OnPlayerSetLocation(PlayerSetLocationEvent playerSetLocationEvent)
        {
            var gameObject = GameObject.Find("Player");
            gameObject.transform.position = playerSetLocationEvent.position;
        }

        public void OnPortalRequestSceneLoad(PortalRequestSceneLoadEvent portalRequestSceneLoadEvent)
        {
            view.StartCoroutine(OnLoadSceneAndWait(portalRequestSceneLoadEvent));
        }

        public void OnDestroy()
        {
            PortalEvent.portalRequestSceneLoadEvent -= OnPortalRequestSceneLoad;
            PlayerEvent.PlayerSetLocationEvent -= OnPlayerSetLocation;
            PortalEvent.portalRequestSceneLoadEvent -= OnPortalRequestSceneLoad;
            PlayerEvent.PlayerSetLocationEvent -= OnPlayerSetLocation;
            SceneEvent.SceneRequestUnLoadEvent -= OnSceneRequestUnLoadEvent;
            SceneEvent.SceneRequestLoadEvent -= OnSceneRequestLoadEvent;
            SceneEvent.SceneRequestTransferEvent -= OnSceneRequestTransferEvent;
            SceneEvent.SceneLoadCompleteEvent -= OnSceneLoadCompleteEvent;
            SceneEvent.SceneUnLoadCompleteEvent -= OnSceneUnLoadCompleteEvent;
        }
    }
}