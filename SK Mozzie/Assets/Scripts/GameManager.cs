using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Audio;
using Unity.Mathematics;

public struct CurrentTargetState
{
    public bool idle, attacked, dead;

    public CurrentTargetState(bool idle, bool attacked, bool dead)
    {
        this.idle = idle;
        this.attacked = attacked;
        this.dead = dead;
    }
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
    public PlayerState PlayerMovement { set => Instance.playerC.SetPlayerState = value; }

    public bool TargetSet => Targets[targetDataIndex].active;

    public bool TargetEnabled { get; set; } = false;

    public TargetDataStruct CurrentTargetData => Targets[targetDataIndex].targetData.tData;

    public CurrentTargetState currentTargetState => Targets[targetDataIndex].GetState();

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

        if (UIManager.Instance == null || playerC == null)
            return;

        UpdateWorldRadius();

        if (PlayerIntroComplete == true)
        {
            SetInitalTarget();
        }
    }

    #region Level Loading. 
    public void LoadMainMenu() => LoadLevel("MainMenu");

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
        TargetEnabled = true;
    }

    public void SetNextTarget()
    {
        targetDataIndex++;
        if (targetDataIndex >= Targets.Count)
        {
            Debug.Log("Game Completed");
            //Resolve game complete. 
            LoadLevel("GameOver");
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

    public float GetBGMVolume()
    {
        return audioManager.GetBGMVolume();
    }

    public float GetSFXVolume()
    {
        return audioManager.GetSFXVolume();
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
