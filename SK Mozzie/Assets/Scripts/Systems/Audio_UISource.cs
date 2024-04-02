using UnityEngine;
using Audio;

namespace UI.Audio
{
    /// <summary>
    /// Class responsible for handling UI audio sound effects. 
    /// </summary>
    public class Audio_UISource : Audio_ABSPoolableSource 
    {
        private bool playing = false;

        public Vector3 position
        {
            get => gameObject.transform.position;
            set => gameObject.transform.position = value;
        }

        public override bool Playing { 
            get => playing; 
            set => playing = value; 
        }

        public override float RemainingTime { 
            get; 
            set; 
        }

        public override void PlayClip(Vector3 position, AudioClip clip)
        {
            gameObject.transform.position = position;
            PlayClip(clip);
        }

        public void PlayClip(AudioClip clip)
        {
            RemainingTime = clip.length;
            source.PlayOneShot(clip);
            playing = true;
        }

        public static Audio_UISource GetNew()
        {
            Audio_UISource uiSource = new GameObject("UIAudioSource").AddComponent<Audio_UISource>();
            AudioSource source = uiSource.gameObject.AddComponent<AudioSource>();
            return uiSource;
        }
    }
}
