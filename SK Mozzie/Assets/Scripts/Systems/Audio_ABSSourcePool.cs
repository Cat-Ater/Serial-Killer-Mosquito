using UnityEngine;

namespace Audio
{
    public abstract class Audio_ABSSourcePool
    {

        public int prepopulationCount = 5; 

        internal abstract void Prepopulate();
        public abstract void PlayClip(AudioClip clip, float maxVolume);
        public abstract void PlayClip(AudioClip clip, SFX_Data data, float maxVolume);
        public abstract void PlayClip(AudioClip clip, Vector3 position, float maxVolume);
        public abstract void PlayClip(AudioClip clip, Vector3 position, SFX_Data data, float maxVolume);
    }
}