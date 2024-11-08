using System;
using Core.Character;
using Core.Character.Events;
using UnityEngine;

namespace Core.Game.QuestManager.Data
{
    [CreateAssetMenu(fileName = "MoveToLocationQuest", menuName = "Quest/MoveToLocationQuest")]
    public class MoveToLocationQuest : Quest
    {
        [SerializeField] public Vector2 targetPosition;
        [SerializeField] public float acceptanceRange;
        [SerializeField] public GameObject movePositionQuestMarkerPrefab;

        private GameObject movePositionQuestMarkerInstance;


        public override void StartQuest()
        {
            base.StartQuest();
            Debug.Log("Starting MoveToLocationMission: " + questName);
            PlayerEvent.PlayerMoveEvent += OnPlayerMoveEvent;
            movePositionQuestMarkerInstance = Instantiate(movePositionQuestMarkerPrefab,
                new Vector3(targetPosition.x, targetPosition.y, 0),
                Quaternion.identity);
        }


        private void OnPlayerMoveEvent(PlayerMoveEvent playerMoveEvent)
        {
            var distance = Vector2.Distance(playerMoveEvent.position, targetPosition);
            if (distance < acceptanceRange)
            {
                CompleteQuest();
            }
        }

        public override void TerminateQuest()
        {
            base.TerminateQuest();
            Destroy(movePositionQuestMarkerInstance);
        }


        public override void CompleteQuest()
        {
            base.CompleteQuest();
            Destroy(movePositionQuestMarkerInstance);
            Debug.Log("Complete MoveToLocationMission: " + questName);
            PlayerEvent.PlayerMoveEvent -= OnPlayerMoveEvent;
        }

        private void OnDestroy()
        {
            PlayerEvent.PlayerMoveEvent -= OnPlayerMoveEvent;
        }
    }
}