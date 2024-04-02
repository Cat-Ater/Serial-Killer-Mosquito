using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.System;
using UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public UI_DialogueDisplay _dialogueManager;
    

    public static UIManager Instance
    {
        get => _instance;
    }

    public static AudioClip PlaySound
    {
        set => Debug.Log("UIManger: Implement UI audio management");
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
