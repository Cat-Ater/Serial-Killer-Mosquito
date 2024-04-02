using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.System;
using UI;
using UI.Audio;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public UI_DialogueDisplay _dialogueManager;
    public UIAudioManager _audioManager;  

    public static UIManager Instance
    {
        get => _instance;
    }

    public static AudioClip PlaySound
    {
        set => _instance._audioManager.PlayClip(_instance.transform.position, value);
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
        }
        else
        {
            DestroyImmediate(this);
        }
    }
}
