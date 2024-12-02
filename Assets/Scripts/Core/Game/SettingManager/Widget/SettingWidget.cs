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
                settingManager.settingsData.masterVolume = volume;
                masterVolumePercentage.text = string.Format("{0:0}%", volume * 100);
                float dB = Mathf.Lerp(-80f, 20f, volume); // 将0-1范围映射到-80dB到20dB
                settingManager.audioMixer.SetFloat("MasterVolume", dB);
            }
        }

        public void SetSfxVolume(float volume)
        {
            if (settingManager)
            {
                settingManager.settingsData.sfxVolume = volume;
                sfxVolumePercentage.text = string.Format("{0:0}%", volume * 100);
                float dB = Mathf.Lerp(-80f, 20f, volume);
                settingManager.audioMixer.SetFloat("SFXVolume", dB);
            }
        }

        public void SetMusicVolume(float volume)
        {
            if (settingManager)
            {
                settingManager.settingsData.musicVolume = volume;
                musicVolumePercentage.text = string.Format("{0:0}%", volume * 100);
                float dB = Mathf.Lerp(-80f, 20f, volume);
                settingManager.audioMixer.SetFloat("MusicVolume", dB);
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
            SetMasterVolume(settingManager.settingsData.masterVolume);
            SetSfxVolume(settingManager.settingsData.sfxVolume);
            SetMusicVolume(settingManager.settingsData.musicVolume);
            UpdateScrollbar();
        }
    }
}