using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Class responsible for handling Game audio sound effects. 
    /// </summary>
    public class Audio_GameSource : Audio_ABSPoolableSource
    {
        private SFX_Data data; 
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
        public override bool Playing
        {
            get => playing;
            set => playing = value;
        }

        /// <summary>
        /// The remaining time left before completion of the SFX. 
        /// </summary>
        public override float RemainingTime
        {
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
            this.data = data;
            PlayClip(clip, data);
        }

        /// <summary>
        /// Call for a clip to be played at the origin of the audio manager. 
        /// </summary>
        /// <param name="clip"> The clip to be played. </param>
        /// <param name="data"> The data to assign to the audio source. </param>
        public void PlayClip(AudioClip clip, SFX_Data data)
        {
            RemainingTime = clip.length;
            this.data = data;
            source.PlayOneShot(clip);
            playing = true;
        }

        internal override void Update()
        {
            //If the audio clip is not playing return. 
            if (!playing)
                return;

            //Calulate the distance between the player and the item.
            float dist = Vector3.Distance(transform.position, GameManager.Instance.PlayerPosition);

            //Resolve the volume settings. 
            if (dist < data.minDist)
            {
                source.volume = 1;
            }
            else if (dist > data.maxDist)
            {
                source.volume = 0;
            }
            else
            {
                source.volume = Mathf.Clamp(1 - ((dist - data.minDist) / (data.maxDist - data.minDist)), data.minVol, data.maxVol);
            }

            //Call the base upates. 
            base.Update();
        }

        /// <summary>
        /// Get a new instance of the Audio_GameSource. 
        /// </summary>
        public static Audio_GameSource GetNew()
        {
            Audio_GameSource gameSource = new GameObject("UIAudioSource").AddComponent<Audio_GameSource>();
            gameSource.gameObject.AddComponent<AudioSource>();
            return gameSource;
        }
    }
}
