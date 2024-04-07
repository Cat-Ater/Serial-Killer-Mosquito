using System.Collections.Generic;
using UnityEngine;
using Audio;
using UI.Audio;

namespace Audio
{
    public class GMAudioManagement
    {
        
        public class Audio_SettingsData
        {
            public float volumeBGM = 100;
            public float volumeSFX = 100;
            public float volumeNarration = 100;
        }

        public class Audio_BGMHandler
        {
            private List<GameBGM> BGM = new List<GameBGM>();

            public void UpdateVolume(float volume)
            {
                if(BGM.Count > 0)
                {
                    foreach (GameBGM item in BGM)
                    {
                        item.AdjustMaxVolume(volume);
                    }
                }
            }

            public void AddBGM(GameBGM musicPlayer)
            {
                BGM.Add(musicPlayer);
            }

            public void ClearBGM()
            {
                BGM = new List<GameBGM>();
            }
        }

        public class Audio_GameSFXSystem : Audio_ABSSourcePool
        {
            /// <summary>
            /// The current sources held by the manager.
            /// </summary>
            public List<Audio_GameSource> sources;

            public Audio_GameSFXSystem()
            {
                sources = new List<Audio_GameSource>();
                Prepopulate();
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
            public override void PlayClip(AudioClip clip, SFX_Data data, float maxVolume)
            {
                Audio_GameSource gAS = GetInactiveSource();
                gAS.position = Vector2.zero;
                gAS.gameObject.SetActive(true);
                gAS.PlayClip(clip, data, maxVolume);
            }

            /// <summary>
            /// Play an audio clip at a set position. 
            /// </summary>
            /// <param name="clip"> The clip to play. </param>
            /// <param name="position"> The position to play the clip at. </param>
            /// <param name="data"> The data defining the bounds of the audio clip's volume. </param>
            public override void PlayClip(AudioClip clip, Vector3 position, SFX_Data data, float maxVolume)
            {
                Audio_GameSource gAS = GetInactiveSource();
                gAS.position = position;
                gAS.gameObject.SetActive(true);
                gAS.PlayClip(clip, data, maxVolume);
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

            public override void PlayClip(AudioClip clip, float maxVolume)
            {
            }

            public override void PlayClip(AudioClip clip, Vector3 position, float maxVolume)
            {
            }

            public void UpdateVolume(float value)
            {
                foreach (Audio_GameSource item in sources)
                {
                    item.EffectMaxVolume = value;
                }
            }

            public void ClearPool()
            {
                sources = new List<Audio_GameSource>();
            }
        }

        /// <summary>
        /// Class responsible for creating, pooling, and managing audio for UI components. 
        /// </summary>
        public class Audio_UISFXManager : Audio_ABSSourcePool
        {
            /// <summary>
            /// The current sources held by the manager.
            /// </summary>
            internal List<Audio_UISource> sources;
            internal Vector2 position = Vector2.zero; 
            public Audio_UISFXManager()
            {
                sources = new List<Audio_UISource>();
                Prepopulate(); 
            }

            /// <summary>
            /// Prepopulates the sources in the manager. 
            /// </summary>
            internal override void Prepopulate()
            {
                for (int i = 0; i < base.prepopulationCount; i++)
                {
                    sources.Add(Audio_UISource.GetNew());
                }
            }

            /// <summary>
            /// Play an audio clip. 
            /// </summary>
            /// <param name="clip"> The audio clip to play. </param>
            public override void PlayClip(AudioClip clip, float maxVolume)
            {
                Audio_UISource uiAS = GetInactiveSource();
                uiAS.position = position;
                uiAS.gameObject.SetActive(true);
                uiAS.PlayClip(clip, maxVolume);
            }

            /// <summary>
            /// Play an audio clip at a set position. 
            /// </summary>
            /// <param name="clip"> The clip to play. </param>
            /// <param name="position"> The position to play the clip at. </param>
            public override void PlayClip(AudioClip clip, Vector3 position, float maxVolume)
            {
                Audio_UISource uiAS = GetInactiveSource();
                uiAS.position = position;
                uiAS.gameObject.SetActive(true);
                uiAS.PlayClip(clip, maxVolume);
            }

            /// <summary>
            /// Returns an inactive source. 
            /// </summary>
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

            public override void PlayClip(AudioClip clip, SFX_Data data, float maxVolume)
            {
            }

            public override void PlayClip(AudioClip clip, Vector3 position, SFX_Data data, float maxVolume)
            {
            }
        }

        private Audio_SettingsData _gameSettingsData;
        private Audio_UISFXManager _uiSFXManager;
        private Audio_GameSFXSystem _gameSFXSys;
        private Audio_BGMHandler _bgmHandler;
        private bool settingsChanged = false;

        public GMAudioManagement()
        {
            _gameSettingsData = new Audio_SettingsData()
            {
                volumeBGM = 50,
                volumeSFX = 50,
                volumeNarration = 50
            };
            _uiSFXManager = new Audio_UISFXManager();
            _gameSFXSys = new Audio_GameSFXSystem();
            _bgmHandler = new Audio_BGMHandler();
        }

        public void SetBGMVolume(float value)
        {
            _gameSettingsData.volumeBGM = value;
            settingsChanged = true;
        }

        public void SetSFXVolume(float value)
        {
            _gameSettingsData.volumeSFX = value;
            settingsChanged = true;
        }

        public void SetNarrationVolume(float value)
        {
            _gameSettingsData.volumeNarration = value;
            settingsChanged = true;
        }

        public void PlaySFXSoundAt(Vector3 position, AudioClip clip, SFX_Data data)
        {
            _gameSFXSys.PlayClip(clip, position, data, _gameSettingsData.volumeSFX);
        }

        public void PlayUISFX(AudioClip clip)
        {
            _uiSFXManager.PlayClip(clip, _gameSettingsData.volumeSFX);
        }

        public void AddBGM(GameBGM bgm)
        {
            _bgmHandler.AddBGM(bgm);
        }

        public void Update()
        {
            if (settingsChanged)
            {
                _bgmHandler.UpdateVolume(_gameSettingsData.volumeBGM);
                _gameSFXSys.UpdateVolume(_gameSettingsData.volumeSFX);
            }
        }

        public void ClearBGM() => _bgmHandler.ClearBGM();

        public void ClearSFX() => _gameSFXSys.ClearPool();
    }
}
