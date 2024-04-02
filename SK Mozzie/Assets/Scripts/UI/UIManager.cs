using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.System;
using UI;
using UI.Audio;

[RequireComponent(typeof(UI_DialogueDisplay))]
[RequireComponent(typeof(Audio_UISFXManager))]
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public UI_DialogueDisplay _dialogueManager;
    public Audio_UISFXManager _audioManager;  

    public static UIManager Instance
    {
        get => _instance;
    }

    public static AudioClip PlaySound
    {
        set => _instance._audioManager.PlayClip(value, _instance.transform.position);
    }

    public static void SetDialogue(IDialogueCaller caller, UIDialogueData data)
    {
        _instance._dialogueManager.SetInstanceUp(caller, data);
    }

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
}
