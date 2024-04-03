using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IDialogueCaller {
    bool isTriggerable = true; 
    public UIDialogueData dialogueData;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && isTriggerable)
            TriggerDialogue();
    }

    public void TriggerDialogue ()
	{
        isTriggerable = false;
        UIManager.SetDialogue(this, dialogueData);
	}

    public void DisplayComplete()
    {
        StartCoroutine(WaitTillRetriggered());
    }

    private IEnumerator WaitTillRetriggered()
    {
        yield return new WaitForSeconds(0.5f);
        isTriggerable = true;
    }

}
