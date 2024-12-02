using UnityEngine;

namespace Core.Game.SettingManager.Handler
{
    public class SettingPropertyHandler
    {
        SettingManager settingManager { get; set; }

        public SettingPropertyHandler(SettingManager settingManager)
        {
            this.settingManager = settingManager;
        }

        public void UpdateMasterVolume(float volume)
        {
            settingManager.settingsData.masterVolume = volume;
            float dB = Mathf.Lerp(-80f, 20f, volume); // 将0-1范围映射到-80dB到20dB
            settingManager.audioMixer.SetFloat("MasterVolume", dB);
        }

        public void UpdateMusicVolume(float volume)
        {
            settingManager.settingsData.musicVolume = volume;
            float dB = Mathf.Lerp(-80f, 20f, volume);
            settingManager.audioMixer.SetFloat("MusicVolume", dB);
        }

        public void UpdateSfxVolume(float volume)
        {
            settingManager.settingsData.sfxVolume = volume;
            float dB = Mathf.Lerp(-80f, 20f, volume);
            settingManager.audioMixer.SetFloat("SFXVolume", dB);
        }

        /// <summary>
        /// Read the SettingData and apply to handlers, this methods read serialize data
        /// and also fix the issue that data not apply when widget is not active
        /// </summary>
        public void LoadSettingProperties()
        {
            UpdateMasterVolume(settingManager.settingsData.masterVolume);
            UpdateSfxVolume(settingManager.settingsData.sfxVolume);
            UpdateMusicVolume(settingManager.settingsData.musicVolume);
        }

        public void Destroy()
        {
        }
    }
}