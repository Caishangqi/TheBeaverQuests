using System;
using System.Collections.Generic;
using Core.Game.AudioManager.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Game.AudioManager
{
    [CreateAssetMenu(fileName = "AudioSo", menuName = "CustomSoundClip/AudioSo")]
    public class AudioSo : ScriptableObject
    {
        [Header("Sound Event List")] public List<AudioData> keyValuePairs;
        public Dictionary<ESound, AudioData> soundList = new Dictionary<ESound, AudioData>();
    }
}

[Serializable]
public struct AudioData
{
    [SerializeField] public ESound eSoundEnum;
    [SerializeField] public AudioClip audioClip;
    [SerializeField] public float delayTime;
}