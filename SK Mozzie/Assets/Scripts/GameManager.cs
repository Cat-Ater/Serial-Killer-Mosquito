using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Audio;
using Unity.Mathematics;

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
    [HideInInspector] public AttackVisualisation attackVisualisation;
    static GameManager _instance;
    GMAudioManagement audioManager;
    public PlayerController playerC;
    public CameraData[] cameraData;
    public List<TargetController> Targets;
    public int targetDataIndex = 0;
    public PlayerRaidalDistance playerRaidusWorld;
    public bool PlayerIntroComplete = false;

    #region Singleton. 
    public static GameManager Instance => _instance;
    #endregion

    #region Player Properties. 
    public Vector3 PlayerPosition { get => playerC.Position; }
    public PlayerState DisablePlayer { set => Instance.playerC.SetPlayerState = value; }

    #endregion

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

        //Generate Audio Setup. 
        audioManager = new GMAudioManagement();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            UIManager.Instance.PauseMenuToggle();

        if (UIManager.Instance == null || playerC == null)
            return;

        UpdateWorldRadius();

        if (PlayerIntroComplete == true)
        {
            SetInitalTarget();
        }
    }

    #region Level Loading. 
    public void LoadLevel(string name)
    {
        //Clear audio. 
        audioManager.ClearBGM();
        audioManager.ClearSFX();

        //Load the scene. 
        SceneManager.LoadScene(name);

        if (name == "Main_Scene")
            LoadUI();
    }

    #endregion

    #region Draw Gizmos. 
    public void OnDrawGizmos()
    {
        Color color = Color.green;
        color.a = 0.5F;
        Gizmos.color = color;
        if (playerRaidusWorld.centerPos != null)
            Gizmos.DrawSphere(playerRaidusWorld.centerPos.position, playerRaidusWorld.radius);
    }
    #endregion

    #region Target Handling. 

    public void SetInitalTarget()
    {
        targetDataIndex = 0;
        Targets[targetDataIndex].SetActive();
    }

    public void SetNextTarget()
    {
        targetDataIndex++;
        if (targetDataIndex >= Targets.Count)
        {
            Debug.Log("Game Completed");
            //Resolve game complete. 
        }
        else
        {
            Targets[targetDataIndex].SetActive();
        }
    }
    #endregion

    #region World Radius. 
    private void UpdateWorldRadius()
    {
        if (playerRaidusWorld.UpdateRadius(playerC.Position))
            LoadLevel("MainMenu");
    }
    #endregion

    #region Camera. 
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

    #endregion

    #region UI. 

    internal void LoadUI()
    {
        SceneManager.LoadSceneAsync(UI_SCENE_NAME, LoadSceneMode.Additive);
    }

    public string KillTagLine { set => UIManager.Instance.SetTargetKillLine = value; }

    #endregion

    #region Audio Management.

    public GameBGM AddBGM
    {
        set => audioManager.AddBGM(value);
    }

    public void PlaySoundFXAt(Vector3 position, AudioClip clip, SFX_Data data)
    {
        audioManager.PlaySFXSoundAt(position, clip, data);
    }

    public void PlayUISFX(AudioClip clip)
    {
        audioManager.PlayUISFX(clip);
    }

    public void SetVolumeLevels(float sfxVolume, float BGMVolume)
    {
        float remapBGM = math.remap(0, 100, 0, 1, BGMVolume);
        float remapSFX = math.remap(0, 100, 0, 1, sfxVolume);

        audioManager.SetBGMVolume(remapBGM);
        audioManager.SetSFXVolume(remapSFX);
    }
    #endregion
}
