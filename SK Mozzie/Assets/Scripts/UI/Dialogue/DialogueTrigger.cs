using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IDialogueCaller {

    public UIDialogueData dialogueData; 

    public void TriggerDialogue ()
	{
        UIManager.SetDialogue(this, dialogueData);
	}

    public void DisplayComplete()
    {

    }

}
