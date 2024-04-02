using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Audio;

public class GameSettingsData
{
    public float volumeBGM;
    public float volumeSFX;
    public float volumeNarration; 
}

public class GameManager : MonoBehaviour
{
    public const string UI_SCENE_NAME = "_UI"; 
    static GameManager _instance;
    public GameObject player; 
    public Audio_GameSFXSystem _gameSFXSys; 
    public PlayerMovementController pMovementController;
    public UIManager uiManager; 

    public Vector3 PlayerPosition { get => player.transform.position; } 
    public static GameManager Instance => _instance;

    public void PlaySoundAt(Vector3 position, AudioClip clip, SFX_Data data)
    {
        _gameSFXSys.PlayClip(clip, position, data);
    }

    internal void LoadUI()
    {
        SceneManager.LoadSceneAsync(UI_SCENE_NAME);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
