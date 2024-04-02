using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Class responsible for handling Game audio sound effects. 
    /// </summary>
    public class Audio_GameSource : Audio_ABSPoolableSource
    {
        private bool playing = false;

        public Vector3 position
        {
            get => gameObject.transform.position;
            set => gameObject.transform.position = value;
        }

        public override bool Playing
        {
            get => playing;
            set => playing = value;
        }

        public override float RemainingTime
        {
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

        public static Audio_GameSource GetNew()
        {
            Audio_GameSource gameSource = new GameObject("UIAudioSource").AddComponent<Audio_GameSource>();
            gameSource.gameObject.AddComponent<AudioSource>();
            return gameSource;
        }
    }
}
