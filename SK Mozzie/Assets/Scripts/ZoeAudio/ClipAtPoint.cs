using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipAtPoint : MonoBehaviour
{
    public AudioClip clip;
    public float volume=1;

    void Start()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
    }
}