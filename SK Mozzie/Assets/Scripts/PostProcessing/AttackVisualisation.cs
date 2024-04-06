using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum ColorType
{
    ESCAPE,
    ATTACK,
    FAILURE
}

public class AttackVisualisation : MonoBehaviour
{
    private Vector2 remapRange = new Vector2(0, 100);
    private Vector2 internalRange = new Vector2(0, 1);
    public Volume volume;
    public Vignette vign;
    public FilmGrain fg;
    public float currentVIntensity = 0;

    public Color attack_Red;
    public Color escape_White;
    public Color failure_Black;

    public float VignetteIntensity
    {
        set
        {
            Debug.Log("Vig Intensity:" + value);
            Debug.Log("Vig Intensity Remap:" + Remap(value, remapRange, internalRange));
            vign.intensity.Override(Remap(value, remapRange, internalRange));
        }
    }

    public float VignetteSmoothness
    {
        set
        {
            Debug.Log("Vig Smoothness:" + value);
            Debug.Log("Vig Smoothness Remap:" + Remap(value, remapRange, internalRange));
            vign.smoothness.Override(Remap(value, remapRange, internalRange));
        }
    }

    private Color SetVigColor
    {
        set
        {
            vign.color = new ColorParameter(value, true);
        }
    }

    public float FilmGrainIntensity
    {
        set
        {
            Debug.Log("FG Intensity:" + value);
            Debug.Log("FG Intensity Remap:" + Remap(value, remapRange, internalRange));
            fg.intensity.Override(Remap(value, remapRange, internalRange));
        }
    }

    public float FilmGrainResponse
    {
        set
        {
            Debug.Log("FG Intensity:" + value);
            Debug.Log("FG Intensity Remap:" + Remap(value, remapRange, internalRange));
            fg.response.Override(Remap(value, remapRange, internalRange));
        }
    }

    void Start()
    {
        GameManager.Instance.attackVisualisation = this;
        GetPostProcessingSettings();
        SetVigColor = attack_Red;
        FilmGrainResponse = 0;
        FilmGrainIntensity = 0;
        VignetteIntensity = 0;
        VignetteSmoothness = 0; 

    }

    private void SetColor(ColorType type)
    {
        switch (type)
        {
            case ColorType.ESCAPE:
                SetVigColor = escape_White;
                break;
            case ColorType.ATTACK:
                SetVigColor = attack_Red;
                break;
            case ColorType.FAILURE:
                SetVigColor = failure_Black;
                break;
            default:
                break;
        }
    }

    private void GetPostProcessingSettings()
    {
        for (int i = 0; i < volume.profile.components.Count; i++)
        {
            if (volume.profile.components[i].GetType() == typeof(Vignette))
            {
                vign = (Vignette)volume.profile.components[i];
            }

            if (volume.profile.components[i].GetType() == typeof(FilmGrain))
            {
                fg = (FilmGrain)volume.profile.components[i];
            }
        }
    }

    public void SetPostProcessingSettings()
    {
        for (int i = 0; i < volume.profile.components.Count; i++)
        {
            if (volume.profile.components[i].GetType() == typeof(Vignette))
            {
                volume.profile.components[i] = vign;
            }

            if (volume.profile.components[i].GetType() == typeof(FilmGrain))
            {
                volume.profile.components[i] = fg;
            }
        }
    }

    public static float Remap(float value, Vector2 from, Vector2 to)
    {
        return math.remap(from.x, from.y, to.x, to.y, value);
    }
}
