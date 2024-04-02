using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

namespace Audio
{
    public class Audio_GameSFXSystem : Audio_ABSSourcePool
    {
        public List<Audio_GameSource> sources;

        internal override void OnEnable()
        {
            sources = new List<Audio_GameSource>();
            base.OnEnable();
        }

        internal override void Prepopulate()
        {
            for (int i = 0; i < base.prepopulationCount; i++)
            {
                sources.Add(Audio_GameSource.GetNew());
            }
        }

        public override void PlayClip(AudioClip clip)
        {
            Audio_GameSource gAS = GetInactiveSource();
            gAS.position = base.Position;
            gAS.gameObject.SetActive(true);
            gAS.PlayClip(clip);
        }

        public override void PlayClip(AudioClip clip, Vector3 position)
        {
            Audio_GameSource gAS = GetInactiveSource();
            gAS.position = position;
            gAS.gameObject.SetActive(true);
            gAS.PlayClip(clip);
        }

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
    }
}




namespace Audio
{
}