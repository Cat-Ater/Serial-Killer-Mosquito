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

        /// <summary>
        /// Accessor for the current position of the audio source. 
        /// </summary>
        public Vector3 position
        {
            get => gameObject.transform.position;
            set => gameObject.transform.position = value;
        }

        /// <summary>
        /// Returns true if the source is active. 
        /// </summary>
        public override bool Playing { 
            get => playing; 
            set => playing = value; 
        }

        /// <summary>
        /// The remaining time left before completion of the SFX. 
        /// </summary>
        public override float RemainingTime { 
            get; 
            set; 
        }

        /// <summary>
        /// Calls for a clip to be played at the given position. 
        /// </summary>
        /// <param name="position"> The position the clip should be played at. </param>
        /// <param name="clip"> The clip to play at said position. </param>
        /// <param name="data"> The data to assign to the audiosource. </param>
        public override void PlayClip(Vector3 position, AudioClip clip, SFX_Data data)
        {
            gameObject.transform.position = position;
            PlayClip(clip);
        }

        /// <summary>
        /// Call for a clip to be played at the origin of the audio manager. 
        /// </summary>
        /// <param name="clip"> The clip to be played. </param>
        /// <param name="data"> The data to assign to the audio source. </param>
        public void PlayClip(AudioClip clip)
        {
            RemainingTime = clip.length;
            source.PlayOneShot(clip);
            playing = true;
        }

        /// <summary>
        /// Get a new instance of the Audio_GameSource. 
        /// </summary>
        public static Audio_UISource GetNew()
        {
            Audio_UISource uiSource = new GameObject("UIAudioSource").AddComponent<Audio_UISource>();
            AudioSource source = uiSource.gameObject.AddComponent<AudioSource>();
            return uiSource;
        }
    }
}
