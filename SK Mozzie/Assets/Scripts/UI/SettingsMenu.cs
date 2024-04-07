using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    GameObject settingsMenuRoot; 
    public Slider musicSlider;
    public Slider SoundEffectSlider; 

    public void ClosePauseMenu()
    {
        //Get volume changes. 
        float bgmVolume = musicSlider.value;
        float sfxVolume = SoundEffectSlider.value;
        //Push volume changes. 
        GameManager.Instance.SetVolumeLevels(sfxVolume, bgmVolume);

        //Close the settings menu. 
        settingsMenuRoot.SetActive(false);
    }
}
