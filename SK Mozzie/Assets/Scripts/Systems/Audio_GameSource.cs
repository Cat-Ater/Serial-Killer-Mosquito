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
        private float maxVolume; 

        /// <summary>
        /// Accessor for the current position of the audio source. 
        /// </summary>
        public Vector3 position
        {
            get => gameObject.transform.position;
            set => gameObject.transform.position = value;
        }

        /// <summary>
        /// The allowed maximum volume. 
        /// </summary>
        public float EffectMaxVolume
        {
            set => maxVolume = value;
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
        public override void PlayClip(Vector3 position, AudioClip clip, SFX_Data data, float maxVolume)
        {
            gameObject.transform.position = position;
            this.data = data;
            PlayClip(clip, data, maxVolume);
        }

        /// <summary>
        /// Call for a clip to be played at the origin of the audio manager. 
        /// </summary>
        /// <param name="clip"> The clip to be played. </param>
        /// <param name="data"> The data to assign to the audio source. </param>
        public void PlayClip(AudioClip clip, SFX_Data data, float maxVolume)
        {
            RemainingTime = clip.length;
            this.data = data;
            this.maxVolume = maxVolume; 

            if (data.playLooped)
            {
                source.loop = true;
                source.clip = clip;
                source.Play();
            }
            else
            {
                source.PlayOneShot(clip);
                playing = true;
            }
        }

        internal override void Update()
        {
            //If the audio clip is not playing return. 
            if (!playing || data.playLooped)
                return;

            //Calulate the distance between the player and the item.
            float dist = Vector3.Distance(transform.position, GameManager.Instance.PlayerPosition);

            float vol;

            //Resolve the volume settings. 
            if (dist < data.minDist)
            {
                vol = 1;
            }
            else if (dist > data.maxDist)
            {
                vol = 0;
            }
            else
            {
                vol = Mathf.Clamp(1 - ((dist - data.minDist) / (data.maxDist - data.minDist)), data.minVol, data.maxVol);
            }

            if (vol > maxVolume)
                vol = maxVolume;

            source.volume = vol; 

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
