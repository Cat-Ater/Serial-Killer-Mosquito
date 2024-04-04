using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SFX_Data
{
    public float minDist = 1;
    public float maxDist = 400;
    public float minVol = 0;
    public float maxVol = 1;
}

public class Game_SFX : MonoBehaviour
{
    [SerializeField] SFX_Data data;

    void Start()
    {
        
    }
}
