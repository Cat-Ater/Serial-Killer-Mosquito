using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

namespace Audio
{
    public class Audio_GameSFXSystem : Audio_ABSSourcePool
    {
        /// <summary>
        /// The current sources held by the manager.
        /// </summary>
        public List<Audio_GameSource> sources;

        internal override void OnEnable()
        {
            sources = new List<Audio_GameSource>();
            base.OnEnable();
        }

        /// <summary>
        /// Prepopulates the sources in the manager. 
        /// </summary>
        internal override void Prepopulate()
        {
            for (int i = 0; i < base.prepopulationCount; i++)
            {
                sources.Add(Audio_GameSource.GetNew());
            }
        }

        /// <summary>
        /// Play an audio clip. 
        /// </summary>
        /// <param name="clip"> The audio clip to play. </param>
        /// <param name="data"> The data defining the bounds of the audio clip's volume. </param>
        public override void PlayClip(AudioClip clip, SFX_Data data)
        {
            Audio_GameSource gAS = GetInactiveSource();
            gAS.position = base.Position;
            gAS.gameObject.SetActive(true);
            gAS.PlayClip(clip, data);
        }

        /// <summary>
        /// Play an audio clip at a set position. 
        /// </summary>
        /// <param name="clip"> The clip to play. </param>
        /// <param name="position"> The position to play the clip at. </param>
        /// <param name="data"> The data defining the bounds of the audio clip's volume. </param>
        public override void PlayClip(AudioClip clip, Vector3 position, SFX_Data data)
        {
            Audio_GameSource gAS = GetInactiveSource();
            gAS.position = position;
            gAS.gameObject.SetActive(true);
            gAS.PlayClip(clip, data);
        }

        /// <summary>
        /// Returns an inactive source. 
        /// </summary>
        private Audio_GameSource GetInactiveSource()
        {
            Audio_GameSource s = null;

            if (sources.Count >= 1)
            {
                for (int i = 0; i < sources.Count; i++)
                {
                    if (sources[i] != null && !sources[i].gameObject.activeSelf)
                    {
                        s = sources[i];
                        break;
                    }
                }
            }

            if (s == null)
            {
                s = Audio_GameSource.GetNew();
                sources.Add(s);
            }
            return s;
        }

        public override void PlayClip(AudioClip clip)
        {
        }

        public override void PlayClip(AudioClip clip, Vector3 position)
        {
        }
    }
}
