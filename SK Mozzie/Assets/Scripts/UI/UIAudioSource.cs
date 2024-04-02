using UnityEngine;

namespace UI.Audio
{
    /// <summary>
    /// Class responsible for handling UI audio sound effects. 
    /// </summary>
    public class UIAudioSource : MonoBehaviour
    {
        private float currentTime;
        private AudioSource source;
        private bool playing = false;

        public Vector3 position
        {
            get => gameObject.transform.position;
            set => gameObject.transform.position = value;
        }

        public void PlayClip(AudioClip clip)
        {
            currentTime = clip.length;
            source.PlayOneShot(clip);
            playing = true;
        }

        public void Update()
        {
            if (!playing)
                return;

            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                gameObject.SetActive(false);
                playing = false;
            }
        }

        public static UIAudioSource GetNew()
        {
            UIAudioSource uiSource = new GameObject("UIAudioSource").AddComponent<UIAudioSource>();
            AudioSource source = uiSource.gameObject.AddComponent<AudioSource>();
            return uiSource;
        }
    }
}
