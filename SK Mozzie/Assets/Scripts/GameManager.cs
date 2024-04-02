using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsData
{
    public float volumeBGM;
    public float volumeSFX;
    public float volumeNarration; 
}

public class GameManager : MonoBehaviour
{
    static GameManager _instance; 

    public GameManager Instance
    {
        get;
        set;
    }

    public 
    public PlayerMovementController pMovementController;
    public UIManager uiManager; 

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
