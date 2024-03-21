using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInputController : MonoBehaviour
{
    private bool initalized = false;

    public int sampleWindow = 20;
    public AudioClip microphoneClip;
    public string device;

    public int SampleWindow
    {
        get => sampleWindow;
        set => sampleWindow = value; 
    }

    public float Average { get; set; }
    public float Peak { get; set; }

    #region Built In Functions.
    private void OnEnable()
    {
        InitMicrophone();
        initalized = true;
    }

    private void OnDisable()
    {
        StopMicrophone();
    }

    private void OnDestroy()
    {
        StopMicrophone();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (!initalized)
            {
                InitMicrophone();
                initalized = true;
            }
        }
        if (!focus)
        {
            StopMicrophone();
            initalized = false;
        }
    }
    #endregion

    #region Microphone Handling.
    private void InitMicrophone()
    {
        if (device == null)
            device = Microphone.devices[0];
        microphoneClip = Microphone.Start(device, true, 20, AudioSettings.outputSampleRate);
    }

    private void StopMicrophone()
    {
        Microphone.End(device);
    }
    #endregion

    private void Update()
    {
        Peak = GetPeak(microphoneClip);
        Average = GetAverage(microphoneClip);
    }

    public void MicrophoneToAudioClip()
    {
        string micName = Microphone.devices[0];
        microphoneClip = Microphone.Start(micName, true, 20, AudioSettings.outputSampleRate);
        Debug.Log(micName);
    }

    private float GetPeak(AudioClip clip)
    {
        int pos = Microphone.GetPosition(device);
        int start = pos - sampleWindow;
        if (start < 0)
            return 0;

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, start);

        float peak = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            if (peak < waveData[i] * waveData[i])
                peak = waveData[i] * waveData[i];
        }

        return peak;
    }

    private float GetAverage(AudioClip clip)
    {
        int pos = Microphone.GetPosition(device);
        int start = pos - sampleWindow;
        if (start < 0)
            return 0;

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, start);

        float total = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            total += Mathf.Abs(waveData[i]);
        }

        return total / sampleWindow;
    }

    public static AudioInputController GetInputController()
    {
        GameObject controller = new GameObject("Audio Input Controller");
        return controller.AddComponent<AudioInputController>();
    }
}

