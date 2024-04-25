using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Display_TargetKill : MonoBehaviour
{
    public GameObject rootObject;
    public TextMeshProUGUI textBox; 
    
    public string SetText
    {
        set { DisplayKillNotification(value); }
    }

    private void DisplayKillNotification(string text)
    {
        Debug.Log(text);
        textBox.text = text;
        rootObject.SetActive(true);
        StartCoroutine(DisplayTimer());
    }

    private IEnumerator DisplayTimer()
    {
        yield return new WaitForSeconds(3.5F);
        rootObject.SetActive(false);
        textBox.text = "";
    }
}
