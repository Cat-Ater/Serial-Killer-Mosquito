using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class allowing for limits to be defined for audio detection. 
/// </summary>
[System.Serializable]
public class Limiter
{
    public float min = 0.0025F;
    public float max = 11F;

    public float LimitResult(float value)
    {
        return (value > min && value < max) ? value : 0;
    }
}

/// <summary>
/// Component class used to detect audio input from microphone.
/// </summary>
public class AudioInputController : MonoBehaviour
{
    private AudioClip microphoneClip;
    private Vector2 remapInitalRange = new Vector2(0F, 0.01F);
    private Vector2 remapOutputRange = new Vector2(0F, 100F);
    private bool initalized = false;
    private int sampleWindow = 10;
    private string device;
    public Limiter detectionLimiter; 

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
        device = Microphone.devices[0];
        microphoneClip = Microphone.Start(device, true, 20, AudioSettings.outputSampleRate);
        Debug.Log(device);
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

        return detectionLimiter.LimitResult(peak);
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

        float t = Remap(total / sampleWindow, remapInitalRange, remapOutputRange);

        return detectionLimiter.LimitResult(t);
    }

    public static AudioInputController GetInputController()
    {
        GameObject controller = new GameObject("Audio Input Controller");
        return controller.AddComponent<AudioInputController>();
    }

    private static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private static float Remap(float value, Vector2 rangeA, Vector2 rangeB)
    {
        return Remap(value, rangeA.x, rangeA.y, rangeB.x, rangeB.y);
    }
}

