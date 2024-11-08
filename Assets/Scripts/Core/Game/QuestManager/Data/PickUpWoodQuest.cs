using System;
using System.Collections.Generic;
using Core.UI.Event;
using UnityEngine;

namespace Core.Game.QuestManager.Data
{
    [CreateAssetMenu(fileName = "PickUpWoodQuest", menuName = "Quest/PickUpWoodQuest")]
    public class PickUpWoodQuest : Quest
    {
        [SerializeField] public bool canPickAnyMountOfWoodToCompleteQuest;
        [SerializeField] public int amountOfWoodToComplete;

        // TODO: CHeck whether level designers need the wood be marked
        private int currentWood = 0;

        public override void StartQuest()
        {
            base.StartQuest();
            UIEvent.InventoryCollectEvent += OnWoodCollected;
            Debug.Log("Starting PickUpWoodQuest: " + questName);
        }

        private void OnWoodCollected(InventoryCollectEvent inventoryCollectEvent)
        {
            currentWood++;
            if (canPickAnyMountOfWoodToCompleteQuest)
            {
                CompleteQuest();
                return;
            }

            if (!canPickAnyMountOfWoodToCompleteQuest)
            {
                if (currentWood >= amountOfWoodToComplete)
                {
                    CompleteQuest();
                }
            }
        }

        public override void TerminateQuest()
        {
            base.TerminateQuest();
        }

        public override void CompleteQuest()
        {
            base.CompleteQuest();
            UIEvent.InventoryCollectEvent -= OnWoodCollected;
            Debug.Log("Complete PickUpWoodQuest: " + questName);
        }

        private void OnDestroy()
        {
            UIEvent.InventoryCollectEvent -= OnWoodCollected;
        }
    }
}