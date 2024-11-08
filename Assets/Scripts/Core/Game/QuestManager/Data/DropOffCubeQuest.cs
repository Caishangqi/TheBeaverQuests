using System;
using Core.Character;
using Core.Character.Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Game.QuestManager.Data
{
    [CreateAssetMenu(fileName = "DropOffCubeQuest", menuName = "Quest/DropOffCubeQuest")]
    public class DropOffCubeQuest : Quest
    {
        [SerializeField] public bool canDropCubeToAnyPositionCompleteQuest;
        [SerializeField] public bool markedTheTargetDropLocation;
        [SerializeField] private GameObject cubeMarkerPrefab;
        [SerializeField] private Vector3 targetDropCubePosition; // Level designer manually inputs this
        [SerializeField] public float positionTolerance = 0.5f; // Tolerance value
        private GameObject cubeMarkerInstance = null;
        private GameObject carriedCubeInstance = null;

        public override void StartQuest()
        {
            base.StartQuest();
            PlayerEvent.PlayerLayDownEvent += OnDropOffCubeEvent;
            Debug.Log("Starting DropOffCubeQuest: " + questName);
            if (!canDropCubeToAnyPositionCompleteQuest && markedTheTargetDropLocation)
            {
                cubeMarkerInstance = Instantiate(cubeMarkerPrefab,
                    targetDropCubePosition + new Vector3(0, 1.0f, 0), Quaternion.identity);
            }
        }

        private void OnDropOffCubeEvent(PlayerLayDownCubeEvent playerLayDownCubeEvent)
        {
            carriedCubeInstance = playerLayDownCubeEvent.cube.gameObject;
            if (canDropCubeToAnyPositionCompleteQuest)
            {
                CompleteQuest();
                return;
            }

            // Check if the object is within the tolerance range
            float distance = Vector3.Distance(playerLayDownCubeEvent.layDownPosition, targetDropCubePosition);
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
            PlayerEvent.PlayerLayDownEvent -= OnDropOffCubeEvent;
            Destroy(cubeMarkerInstance);
            Debug.Log("Complete DropOffCubeQuest: " + questName);
        }

        private void OnDestroy()
        {
            PlayerEvent.PlayerLayDownEvent -= OnDropOffCubeEvent;
        }
    }
}