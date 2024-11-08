using Core.Game.AudioManager.Data;
using UnityEngine;

namespace Core.Game.AudioManager.Events
{
    public struct PlaySoundEvent
    {
        public GameObject instigator { get; set; }
        public ESound ESound { get; set; }
        public Vector3 position { get; set; }
        public float delay { get; set; }

        public PlaySoundEvent(GameObject instigator, ESound eSound, Vector3 position, float delay)
        {
            this.instigator = instigator;
            this.ESound = eSound;
            this.position = position;
            this.delay = delay;
        }

        public PlaySoundEvent(GameObject instigator, ESound eSound, float delay)
        {
            this.instigator = instigator;
            this.ESound = eSound;
            position = Vector3.zero;
            this.delay = delay;
        }

        public PlaySoundEvent(GameObject instigator, ESound eSound)
        {
            this.instigator = instigator;
            this.ESound = eSound;
            position = Vector3.zero;
            this.delay = 0;
        }
    }
}