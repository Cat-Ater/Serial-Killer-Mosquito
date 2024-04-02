using System.Collections.Generic;
using UnityEngine;

namespace UI.Audio
{
    /// <summary>
    /// Class responsible for creating, pooling, and managing audio for UI components. 
    /// </summary>
    public class UIAudioManager : MonoBehaviour
    {
        public List<UIAudioSource> sources;
        public int prepoulationCount = 5;

        private void OnEnable()
        {
            sources = new List<UIAudioSource>();
            Prepopulate();
        }

        private void Prepopulate()
        {
            //Prepopulate. 
            UIAudioSource s;

            for (int i = 0; i < prepoulationCount; i++)
            {
                s = UIAudioSource.GetNew();
                sources.Add(s);
            }
        }

        /// <summary>
        /// Play a UI audio clip. 
        /// </summary>
        /// <param name="pos"> The position in space to play the clip at (Vector3). </param>
        /// <param name="clip"> The AudioClip to play at the position. </param>
        public void PlayClip(Vector3 pos, AudioClip clip)
        {
            UIAudioSource uiAS = GetSource();
            uiAS.position = pos;
            uiAS.gameObject.SetActive(true);
            uiAS.PlayClip(clip);
        }

        /// <summary>
        /// Retrive an audio source from the pool. 
        /// </summary>
        private UIAudioSource GetSource()
        {
            UIAudioSource s = null;

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
                s = UIAudioSource.GetNew();
                sources.Add(s);
            }
            return s;
        }
    }
}
