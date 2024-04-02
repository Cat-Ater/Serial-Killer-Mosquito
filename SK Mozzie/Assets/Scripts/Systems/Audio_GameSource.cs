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

        public override void PlayClip(Vector3 position, AudioClip clip, SFX_Data data)
        {
            gameObject.transform.position = position;
            this.data = data;
            PlayClip(clip, data);
        }

        public void PlayClip(AudioClip clip, SFX_Data data)
        {
            RemainingTime = clip.length;
            this.data = data;
            source.PlayOneShot(clip);
            playing = true;
        }

        internal override void Update()
        {
            float dist = Vector3.Distance(transform.position + (Vector3)offset, Player);

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
        }

        public static Audio_GameSource GetNew()
        {
            Audio_GameSource gameSource = new GameObject("UIAudioSource").AddComponent<Audio_GameSource>();
            gameSource.gameObject.AddComponent<AudioSource>();
            return gameSource;
        }
    }
}
