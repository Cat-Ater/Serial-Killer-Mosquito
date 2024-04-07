using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGM : MonoBehaviour
{
    public AudioSource source; 

    void Start()
    {
        GameManager.Instance.AddBGM = this;
    }

    public void AdjustMaxVolume(float value)
    {
        source.volume = value; 
    }
}
