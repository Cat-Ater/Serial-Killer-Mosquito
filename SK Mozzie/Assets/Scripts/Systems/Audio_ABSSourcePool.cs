using UnityEngine;

namespace Audio
{
    public abstract class Audio_ABSSourcePool : MonoBehaviour
    {

        public int prepopulationCount = 5; 
        public Vector2 Position => gameObject.transform.position;

        internal virtual void OnEnable()
        {
            Prepopulate(); 
        }

        internal abstract void Prepopulate();
        public abstract void PlayClip(AudioClip clip);
        public abstract void PlayClip(AudioClip clip, SFX_Data data);
        public abstract void PlayClip(AudioClip clip, Vector3 position);
        public abstract void PlayClip(AudioClip clip, Vector3 position, SFX_Data data);
    }
}