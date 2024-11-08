using System;
using System.Collections.Generic;
using System.ComponentModel;
using Core.UI.PopUpWidget;
using Core.UI.PopUpWidget.Event;
using UnityEngine;

namespace Core.Game.QuestManager.Data
{
    /// <summary>
    /// The CheckOutPopUpWidgetQuest first display a popup widget
    /// , usually when player click / close the popup widget, the
    /// task will mark as complete
    /// </summary>
    [CreateAssetMenu(fileName = "CheckOutPopUpWidgetQuest", menuName = "Quest/CheckOutPopUpWidgetQuest")]
    public class CheckOutPopUpWidgetQuest : Quest
    {
        [Header("CheckOut Widgets"), Description("CheckOut Widget need to be checked out inorder to finish quest")]
        [SerializeField]
        public GameObject checkOutPopUpWidgetPrefab = null;

        private GameObject checkOutPopUpWidgetInstance = null;
        private bool isLastPage;

        public override void StartQuest()
        {
            base.StartQuest();
            isLastPage = false;
            Debug.Log("Starting CheckOutPopUpWidgetQuest: " + questName);
            checkOutPopUpWidgetInstance = Instantiate(checkOutPopUpWidgetPrefab);
            PopUpEvent.PopUpWidgetClickEvent += OnPopUpWidgetClickEvent;
            PopUpEvent.PopUpWidgetReachLastPageEvent += OnPopUpWidgetReachLastPageEvent;
        }

        private void OnPopUpWidgetReachLastPageEvent(PopUpWidgetReachLastPageEvent popUpWidgetReachLastPageEvent)
        {
            Debug.Log("OnPopUpWidgetReachLastPageEvent: " + popUpWidgetReachLastPageEvent);
            if (popUpWidgetReachLastPageEvent.view.gameObject == checkOutPopUpWidgetInstance)
            {
                isLastPage = true;
            }
        }

        private void OnPopUpWidgetClickEvent(PopUpWidgetClickEvent popUpWidgetClickEvent)
        {
            if (popUpWidgetClickEvent.view.gameObject == checkOutPopUpWidgetInstance && isLastPage)
            {
                CompleteQuest();
            }
        }

        public override void TerminateQuest()
        {
            base.TerminateQuest();
            Destroy(checkOutPopUpWidgetInstance);
        }

        public override void CompleteQuest()
        {
            base.CompleteQuest();
            Destroy(checkOutPopUpWidgetInstance);
            Debug.Log("Complete CheckOutPopUpWidgetQuest: " + questName);
        }

        private void OnDestroy()
        {
            PopUpEvent.PopUpWidgetClickEvent -= OnPopUpWidgetClickEvent;
            PopUpEvent.PopUpWidgetReachLastPageEvent -= OnPopUpWidgetReachLastPageEvent;
        }
    }
}