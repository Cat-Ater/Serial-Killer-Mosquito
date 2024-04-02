using Audio;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Audio
{
    /// <summary>
    /// Class responsible for creating, pooling, and managing audio for UI components. 
    /// </summary>
    public class Audio_UISFXManager : Audio_ABSSourcePool
    {
        public List<Audio_UISource> sources;

        internal override void OnEnable()
        {
            sources = new List<Audio_UISource>();
            base.OnEnable(); 
        }

        internal override void Prepopulate()
        {
            for (int i = 0; i < base.prepopulationCount; i++)
            {
                sources.Add(Audio_UISource.GetNew());
            }
        }

        public override void PlayClip(AudioClip clip)
        {
            Audio_UISource uiAS = GetInactiveSource();
            uiAS.position = base.Position;
            uiAS.gameObject.SetActive(true);
            uiAS.PlayClip(clip);
        }

        public override void PlayClip(AudioClip clip, Vector3 position)
        {
            Audio_UISource uiAS = GetInactiveSource();
            uiAS.position = position;
            uiAS.gameObject.SetActive(true);
            uiAS.PlayClip(clip);
        }

        private Audio_UISource GetInactiveSource()
        {
            Audio_UISource s = null;

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
                s = Audio_UISource.GetNew();
                sources.Add(s);
            }
            return s;
        }
    }
}
