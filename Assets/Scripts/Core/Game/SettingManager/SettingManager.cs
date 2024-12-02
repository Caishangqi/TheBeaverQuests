using Core.Game.SettingManager.Data;
using Core.Game.SettingManager.Events;
using Core.Game.SettingManager.Widget;
using UnityEngine;
using UnityEngine.Audio;

namespace Core.Game.SettingManager
{
    public class SettingManager : MonoBehaviour
    {
        #region Setting Manager

        #region Components

        [SerializeField] public SettingsData settingsData;
        [Header("Audio")] [SerializeField] public AudioMixer audioMixer;
        [Header("Widget")] [SerializeField] public SettingWidget settingWidget;

        #endregion

        #region Handler

        #endregion

        #region Variable

        #endregion

        public void DisplaySettingWidget()
        {
            settingWidget.gameObject.SetActive(true);
        }

        public void HideSettingWidget()
        {
            settingWidget.gameObject.SetActive(false);
        }

        #endregion

        //🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹🥹

        #region Singleton

        public static SettingManager instance { get; private set; }

        /// <summary>
        /// We need ensure that the SettingManager is global and singleton and initialize
        /// at first
        /// </summary>
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            SettingManagerEvent.RequestSettingWidgetEvent += OnRequestSettingWidgetEvent;
        }

        private void OnRequestSettingWidgetEvent(RequestSettingWidgetEvent requestSettingWidgetEvent)
        {
            if (requestSettingWidgetEvent.requestState)
            {
                DisplaySettingWidget();
            }
            else
            {
                HideSettingWidget();
            }
        }

        private void OnDestroy()
        {
            SettingManagerEvent.RequestSettingWidgetEvent -= OnRequestSettingWidgetEvent;
        }

        #endregion
    }
}