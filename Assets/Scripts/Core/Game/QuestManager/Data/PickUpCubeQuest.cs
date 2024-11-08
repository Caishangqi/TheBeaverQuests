using System;
using System.Collections;
using Core.Character;
using Core.Character.Events;
using UnityEngine;

namespace Core.Game.QuestManager.Data
{
    [CreateAssetMenu(fileName = "PickUpCubeQuest", menuName = "Quest/PickUpCubeQuest")]
    public class PickUpCubeQuest : Quest
    {
        [SerializeField] public bool canPickAnyCubeToCompleteQuest;
        [SerializeField] public bool highLightedTheTargetCube;
        [SerializeField] public bool markedTheTargetCube;
        [SerializeField] private Vector3 targetCubePosition; // Level designer manually inputs this
        [SerializeField] private GameObject cubeMarkerPrefab;

        [SerializeField] public float positionTolerance = 0.5f; // Tolerance value

        private GameObject cubeMarkerInstance = null;
        private GameObject carriedCubeInstance = null;

        public override void StartQuest()
        {
            base.StartQuest();
            PlayerEvent.PlayerCarryEvent += OnPlayerCarryEvent;
            Debug.Log("Starting PickUpCubeQuest: " + questName);
            if (!canPickAnyCubeToCompleteQuest && markedTheTargetCube)
            {
                cubeMarkerInstance = Instantiate(cubeMarkerPrefab,
                    targetCubePosition + new Vector3(0f, 1f, 0f),
                    Quaternion.identity);
            }
        }

        private void OnPlayerCarryEvent(PlayerCarryCubeEvent playerCarryCubeEvent)
        {
            carriedCubeInstance = playerCarryCubeEvent.gameObject;

            if (canPickAnyCubeToCompleteQuest)
            {
                CompleteQuest();
                return;
            }

            // Check if the object is within the tolerance range
            float distance = Vector3.Distance(carriedCubeInstance.transform.position, targetCubePosition);
            if (distance <= positionTolerance)
            {
                CompleteQuest();
            }
        }

        public override void TerminateQuest()
        {
            base.TerminateQuest();
            Destroy(cubeMarkerInstance);
        }

        public override void CompleteQuest()
        {
            base.CompleteQuest();
            PlayerEvent.PlayerCarryEvent -= OnPlayerCarryEvent;
            Destroy(cubeMarkerInstance);
            Debug.Log("Complete PickUpCubeQuest: " + questName);
        }

        private void OnDestroy()
        {
            PlayerEvent.PlayerCarryEvent -= OnPlayerCarryEvent;
        }
    }
}