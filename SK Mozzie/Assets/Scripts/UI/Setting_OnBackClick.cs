using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_OnBackClick : MonoBehaviour
{

    public void OnClick()
    {
        UIManager.Instance.settingsMenu.ClosePauseMenu();
    }
}
