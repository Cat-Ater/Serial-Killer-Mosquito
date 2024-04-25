using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responible for managing background music. 
/// </summary>
public class GameBGM : MonoBehaviour
{
    public AudioSource source; 

    void Start()
    {
        GameManager.Instance.AddBGM = this;
    }

    /// <summary>
    /// Set the BGM volume. 
    /// </summary>
    /// <param name="value"> The volume to set. </param>
    public void AdjustMaxVolume(float value)
    {
        source.volume = value; 
    }
}
