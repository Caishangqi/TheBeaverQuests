using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Game.SettingManager.Widget
{
    /// <summary>
    /// Setting Widget that player could control 3 scrollbar and one toggle UI
    /// </summary>
    public class SettingWidget : MonoBehaviour
    {
        #region Components

        [Header("Settings Percentage")] [SerializeField]
        public TextMeshProUGUI masterVolumePercentage;

        [SerializeField] public TextMeshProUGUI sfxVolumePercentage;
        [SerializeField] public TextMeshProUGUI musicVolumePercentage;

        [Header("Settings ScrollBar")] [SerializeField]
        public Scrollbar masterVolumeScrollbar;

        [SerializeField] public Scrollbar sfxVolumeScrollbar;
        [SerializeField] public Scrollbar musicVolumeScrollbar;
        [SerializeField] public Toggle restoreCameraToggle;

        #endregion

        SettingManager settingManager;

        public void SetMasterVolume(float volume)
        {
            if (settingManager)
            {
                masterVolumePercentage.text = string.Format("{0:0}%", volume * 100);
                settingManager.settingPropertyHandler.UpdateMasterVolume(volume);
            }
        }

        public void SetSfxVolume(float volume)
        {
            if (settingManager)
            {
                sfxVolumePercentage.text = string.Format("{0:0}%", volume * 100);
                settingManager.settingPropertyHandler.UpdateSfxVolume(volume);
            }
        }

        public void SetMusicVolume(float volume)
        {
            if (settingManager)
            {
                musicVolumePercentage.text = string.Format("{0:0}%", volume * 100);
                settingManager.settingPropertyHandler.UpdateMusicVolume(volume);
            }
        }

        public void SetEnableCameraRestore()
        {
            if (settingManager)
            {
                settingManager.settingsData.enableCameraRestore = !settingManager.settingsData.enableCameraRestore;
                restoreCameraToggle.isOn = settingManager.settingsData.enableCameraRestore;
            }
        }

        public void CloseSettingWidget()
        {
            gameObject.SetActive(false);
        }

        private void UpdateScrollbar()
        {
            masterVolumeScrollbar.value = settingManager.settingsData.masterVolume;
            sfxVolumeScrollbar.value = settingManager.settingsData.sfxVolume;
            musicVolumeScrollbar.value = settingManager.settingsData.musicVolume;
        }

        void Start()
        {
            settingManager = SettingManager.instance;
            UpdateScrollbar();
        }
    }
}