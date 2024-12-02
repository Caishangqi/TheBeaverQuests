using UnityEngine;

namespace Core.Game.SettingManager.Data
{
    [CreateAssetMenu(fileName = "SettingsData", menuName = "Settings/SettingsData")]
    public class SettingsData : ScriptableObject
    {
        [Range(0f, 1f)] public float masterVolume = 1f;
        [Range(0f, 1f)] public float musicVolume = 1f;
        [Range(0f, 1f)] public float sfxVolume = 1f;
        
        public bool enableCameraRestore = true;
        
    }
}