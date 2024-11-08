using System;

namespace Core.Game.AudioManager.Handler
{
    public class AudioProcessHandler
    {
        AudioManagerView view;

        public AudioProcessHandler(AudioManagerView view)
        {
            this.view = view;
        }

        // Convert the custom Clip list on the Editor to native C# format
        public bool ImportUserContextIntoDictionary()
        {
            try
            {
                foreach (AudioData audioData in view.audioSo.keyValuePairs)
                {
                    view.audioSo.soundList.Add(audioData.eSoundEnum, audioData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }
    }
}