using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsMenuRoot;
    public Slider musicSlider;
    public Slider SoundEffectSlider;

    public bool Open
    {
        get => settingsMenuRoot.activeSelf;
        set => settingsMenuRoot.SetActive(value);
    }

    public void PopulateData()
    {
        //Pull current volume data. 
        float unscaledBGM = GameManager.Instance. GetBGMVolume();
        float unscaledSFX = GameManager.Instance.GetSFXVolume();

        //Remap values. 
        float scaledBGM = Mathf.Clamp(math.remap(0, 1, 0, 100, unscaledBGM), 0, 100);
        float scaledSFX = Mathf.Clamp(math.remap(0, 1, 0, 100, unscaledSFX), 0, 100);

        //Set to the display. 
        musicSlider.value = scaledBGM;
        SoundEffectSlider.value = scaledSFX;
    }

    public void PushData()
    {
        //Get volume changes. 
        float bgmVolume = musicSlider.value;
        float sfxVolume = SoundEffectSlider.value;

        //Push volume changes. 
        GameManager.Instance.SetVolumeLevels(sfxVolume, bgmVolume);
    }
}
