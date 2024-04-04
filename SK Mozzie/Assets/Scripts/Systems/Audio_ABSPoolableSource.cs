using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class Audio_ABSPoolableSource : MonoBehaviour
    {
        internal AudioSource source;
        public abstract bool Playing { get; set; }
        public abstract float RemainingTime { get; set; }

        internal void OnEnable()
        {
            gameObject.name = "UIAudioSource:Active";
            if (source == null)
                source = gameObject.GetComponent<AudioSource>();
        }

        internal void OnDisable()
        {
            gameObject.name = "UIAudioSource:Inactive";
        }

        internal virtual void Update()
        {
            if (!Playing)
                return;

            RemainingTime -= Time.deltaTime;
            if (RemainingTime <= 0)
            {
                Playing = false;
                gameObject.SetActive(false);
            }
        }

        public abstract void PlayClip(Vector3 position, AudioClip clip, SFX_Data data);
    }
}