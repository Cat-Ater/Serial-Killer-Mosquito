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

[System.Serializable]
public struct PlayerRaidalDistance
{
    public Transform centerPos; 
    public float radius; 

    public bool OutsideRadius(Vector3 position)
    {
        Vector3 centre = centerPos.position;
        Vector3 player = position;
        Vector3 distanceVec = centre - player;
        Vector3 unitVec = distanceVec.normalized;
        if( distanceVec.magnitude >= (radius * unitVec).magnitude)
        {
            return true; 
        }

        return false;
    }
}

public class GameManager : MonoBehaviour
{
    public const string UI_SCENE_NAME = "_UI";
    private GameSettingsData gameSettingsData; 
    static GameManager _instance;
    public PlayerController playerC; 
    public Audio_GameSFXSystem _gameSFXSys;
    [HideInInspector] public AttackVisualisation attackVisualisation;
    public CameraData[] cameraData;
    public List<TargetData> Targets;
    public PlayerRaidalDistance playerRaidusWorld;

    public static GameManager Instance => _instance;
    public Vector3 PlayerPosition { get => playerC.Position; }
    public PlayerState DisablePlayer { set => Instance.playerC.SetPlayerState = value; }
    public string KillTagLine { set => UIManager.Instance.SetTargetKillLine = value; }

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

        //Generate game settings data. 

        gameSettingsData = new GameSettingsData()
        {
            volumeBGM = 50,
            volumeSFX = 50,
            volumeNarration = 50
        };
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

    //Current outside zone damange. 
    public float playerDeathSpeed = 0.55f;
    public float currentTimeOutside = 0; 

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            UIManager.Instance.PauseMenuToggle();
        }

        if (UIManager.Instance == null || playerC == null)
            return;

        //Player zone Detection. 
        bool playerOutsideZone = playerRaidusWorld.OutsideRadius(playerC.Position);
        float intensity = 100 / playerDeathSpeed; 

        if (playerOutsideZone)
        {
            currentTimeOutside += Time.deltaTime;

            //Update shader
            attackVisualisation.SetColor(ColorType.FAILURE);
            attackVisualisation.VignetteIntensity = intensity * currentTimeOutside;
            attackVisualisation.SetPostProcessingSettings();

            if (currentTimeOutside >= playerDeathSpeed)
                LoadLevel("MainMenu");
        } 

        if(currentTimeOutside > 0 && !playerOutsideZone)
        {
            currentTimeOutside = 0;
            attackVisualisation.VignetteIntensity = 0;
            attackVisualisation.SetPostProcessingSettings();
        }

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

    public void LoadLevel(string name)
    {

        SceneManager.LoadScene(name);
        if(name == "Main_Scene")
        {
            LoadUI();
        }
    }

    public void OnDrawGizmos()
    {
        Color color = Color.green;
        color.a = 0.5F;
        Gizmos.color = color;
        if(playerRaidusWorld.centerPos != null)
            Gizmos.DrawSphere(playerRaidusWorld.centerPos.position, playerRaidusWorld.radius);
    }
}
