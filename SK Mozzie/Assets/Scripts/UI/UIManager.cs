using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.System;
using UI;
using UI.Audio;

[RequireComponent(typeof(UI_DialogueDisplay))]
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Private singleton reference. 
    /// </summary>
    private static UIManager _instance = null;
    /// <summary>
    /// Reference to the current dialogue manager instance. 
    /// </summary>
    public UI_DialogueDisplay _dialogueManager;
    /// <summary>
    /// Reference to the current UI target display. 
    /// </summary>
    public UI_TargetDisplay _targetDisplay;
    /// <summary>
    /// Reference to the pause menu controller. 
    /// </summary>
    public PauseMenuController _pauseMenuController;
    /// <summary>
    /// Reference to the controller for displaying that a target is assasinated. 
    /// </summary>
    public UI_Display_TargetKill _displayTargetKill;
    public SettingsMenu settingsMenu; 

    #region Singleton. 
    public static UIManager Instance
    {
        get => _instance;
    }
    #endregion

    #region Audio.
    public static AudioClip PlaySound
    {
        set => GameManager.Instance.PlayUISFX(value);
    }
    #endregion

    #region Dialogue Setting.
    public string SetTargetKillLine
    {
        set
        {
            _displayTargetKill.SetText = value;
        }
    }

    public static void SetDialogue(IDialogueCaller caller, UIDialogueData data)
    {
        _instance._dialogueManager.SetInstanceUp(caller, data);
    }
    #endregion

    #region Inbulits. 

    public void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("UI Manager Deloaded");
    }
    #endregion

    #region Pause Menu. 
    public void ClosePauseMenu() =>
        _pauseMenuController.MenuClose();

    public void CloseSettingsMenu() =>
        _pauseMenuController.SettingsMenuClose();

    public void OpenSettingsMenu() =>
        _pauseMenuController.SettingsMenuOpen();
    #endregion

    #region Target Data display. 
    public void SetTargetData(bool idle, bool attacked, bool dead) =>
        _targetDisplay.UpdateData(idle, attacked, dead);
    #endregion
}
