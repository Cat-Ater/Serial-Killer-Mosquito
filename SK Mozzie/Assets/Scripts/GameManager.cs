using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsData
{
    public float volumeBGM = 100;
    public float volumeSFX = 100;
    public float volumeNarration = 100;
}

public class GameManager : MonoBehaviour
{
    static GameManager _instance; 

    public GameManager Instance
    {
        get;
        set;
    }

    
    public PlayerMovementController pMovementController;
    public UIManager uiManager; 

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
