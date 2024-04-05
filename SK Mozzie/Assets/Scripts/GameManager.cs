using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Audio;

public class GameSettingsData
{
    public float volumeBGM = 100;
    public float volumeSFX = 100;
    public float volumeNarration = 100;
}

[System.Serializable]
public class CameraData
{
    public Camera camera;
    public string name;
    public bool active;

    public void Match(string name)
    {
        camera.enabled = (this.name == name);
    }
}

public class GameManager : MonoBehaviour
{
    public const string UI_SCENE_NAME = "_UI";
    static GameManager _instance;
    public PlayerController playerC; 
    public Audio_GameSFXSystem _gameSFXSys;
    public CameraData[] cameraData;
    public List<TargetData> Targets; 

    public static GameManager Instance => _instance;
    public Vector3 PlayerPosition { get => playerC.Position; }

    public void OnEnable()
    {
        if (Instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
        LoadUI();
    }

    public void PlaySoundAt(Vector3 position, AudioClip clip, SFX_Data data)
    {
        _gameSFXSys.PlayClip(clip, position, data);
    }

    internal void LoadUI()
    {
        SceneManager.LoadSceneAsync(UI_SCENE_NAME, LoadSceneMode.Additive);
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void SwitchCamera(string name)
    {
        foreach (CameraData item in cameraData)
        {
            if (item.active)
            {
                item.Match(name);
            }
        }
    }

    public void SetTargetSelectionActive()
    {

    }

    public void SetNextTarget()
    {

    }
}
