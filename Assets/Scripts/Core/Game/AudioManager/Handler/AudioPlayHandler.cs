using System.Collections;
using Core.Game.AudioManager.Events;
using UnityEngine;

namespace Core.Game.AudioManager.Handler
{
    public class AudioPlayHandler
    {
        AudioManagerView view;

        public AudioPlayHandler(AudioManagerView view)
        {
            this.view = view;

            AudioEvent.PlaySoundEvent += OnPlaySoundEvent;
        }

        private void OnPlaySoundEvent(PlaySoundEvent playSoundEvent)
        {
            // Fist Search enum sound to user defined dictionary
            if (view.audioSo.soundList.ContainsKey(playSoundEvent.ESound))
            {
                float delay = view.audioSo.soundList[playSoundEvent.ESound].delayTime;
                view.StartCoroutine(DelayedSoundPlay(delay, playSoundEvent));
            }
        }

        IEnumerator DelayedSoundPlay(float delay, PlaySoundEvent playSoundEvent)
        {
            yield return new WaitForSeconds(delay);

            if (view.audioSource != null)
            {
                view.audioSource.PlayOneShot(view.audioSo.soundList[playSoundEvent.ESound].audioClip);
            }
        }

        
    }
}