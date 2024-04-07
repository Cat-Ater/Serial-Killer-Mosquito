using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource musicVol;

    private float musicVolume = 1.0f;
    void Start()
    {
        musicVol.Play();
    }

    // Update is called once per frame
    void Update()
    {
        musicVol.volume = musicVolume;
    }

    public void updateVolume( float volume)
    {
        musicVolume = volume;
    }
}
