using System.Collections;
using UI;
using UnityEngine;

public class PlayerInitalDialogue : MonoBehaviour, IDialogueCaller
{
    public UIDialogueData dialogueData;
    [HideInInspector] public bool played = false;

    void Update()
    {
        if (!played)
        {
            played = true;
            UIManager.SetDialogue(this, dialogueData);
        }
    }

    public void DisplayComplete()
    {
        Debug.Log("Inital Dialogue Display: Complete.");
    }
}