using System;
using Core.Character;
using Core.Game.ControllerModule.Controller;
using Core.Game.ControllerModule.Manager.Events;
using UnityEngine;

namespace Core.Game.ControllerModule.Manager
{
    public class ControllerManagerView : MonoBehaviour
    {
        private ControllerBase currentController;

        [Header("Objects that will be controlled")] [SerializeField]
        private PlayerView playerView;

        // Start is called before the first frame update
        void Start()
        {
            ControllerEvent.RequestPlayerUnpossessControllerEvent += OnRequestPlayerUnpossessControllerEvent;
            ControllerEvent.RequestPlayerPossessControllerEvent += OnRequestPlayerPossessControllerEvent;

            if (playerView == null)
            {
                playerView = GetComponent<PlayerView>();
                Debug.LogWarning("PlayerView in the Unity Editor is null. I help you set this time :(");
            }

            // Initialize the player controller and give the player control by default
            ControllerBase playerController = new PlayerController(playerView);
            Possess(playerController);
        }

        private void OnRequestPlayerPossessControllerEvent(
            RequestPlayerPossessControllerEvent requestPlayerPossessControllerEvent)
        {
            Possess(requestPlayerPossessControllerEvent.newController);
        }

        private void OnRequestPlayerUnpossessControllerEvent(
            RequestPlayerUnpossessControllerEvent requestPlayerUnpossessControllerEvent)
        {
            Unpossess();
        }

        // Update is called once per frame
        void Update()
        {
            currentController?.ProcessInput();
        }

        // Possess: Take control of a controller
        public void Possess(ControllerBase newController)
        {
            if (currentController != null)
            {
                currentController.OnUnpossess();
            }

            currentController = newController;
            currentController.OnPossess();
        }

        public void Unpossess()
        {
            if (currentController != null)
            {
                currentController.OnUnpossess();
                currentController = null;
            }
        }

        private void OnDestroy()
        {
            ControllerEvent.RequestPlayerUnpossessControllerEvent -= OnRequestPlayerUnpossessControllerEvent;
            ControllerEvent.RequestPlayerPossessControllerEvent -= OnRequestPlayerPossessControllerEvent;
        }
    }
}