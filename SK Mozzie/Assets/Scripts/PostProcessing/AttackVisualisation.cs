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

    public Color attack_Red;
    public Color escape_White;
    public Color failure_Black;

    private float VignetteIntensity
    {
        set { vign.intensity = new ClampedFloatParameter(Remap(value, internalRange, remapRange), 0, 1, true); }
    }

    private float VignetteSmoothness
    {
        set { vign.smoothness = new ClampedFloatParameter(Remap(value, internalRange, remapRange), 0, 1, true); }
    }

    private Color SetVigColor
    {
        set
        {
            vign.color = new ColorParameter(value, true);
        }
    }

    private float VignetteAttackAlpha
    {
        set
        {
            Color c = attack_Red;
            c.a = value;
            SetVigColor = c;
        }
    }

    private float VignetteEscapeAlpha
    {
        set
        {
            Color c = escape_White;
            c.a = value;
            SetVigColor = c;
        }
    }

    private float VignetteFailureColor
    {
        set
        {
            Color c = failure_Black;
            c.a = value;
            SetVigColor = c;
        }
    }

    private float FilmGrainIntensity
    {
        set => fg.intensity = new ClampedFloatParameter(Remap(value, internalRange, remapRange), 0, 1, true);
    }

    private float FilmGrainResponse
    {
        set => fg.response = new ClampedFloatParameter(Remap(value, internalRange, remapRange), 0, 1, true);
    }

    void Start()
    {
        GetPostProcessingSettings();
        SetAttackState(ColorType.ATTACK, 0, 0, 0, 0, 0);
    }

    public void SetAttackState(ColorType colorType, float intensityVig, float smoothnessVig, float intensityFilmGrain, float responseFilmGrain, float alpha)
    {
        Debug.Log("Attack call detected");
        VignetteSmoothness = smoothnessVig;
        VignetteIntensity = intensityVig;
        switch (colorType)
        {
            case ColorType.ESCAPE:
                VignetteEscapeAlpha = alpha;
                break;
            case ColorType.ATTACK:
                VignetteAttackAlpha = alpha;
                break;
            case ColorType.FAILURE:
                VignetteFailureColor = alpha;
                break;
            default:
                break;
        }
        FilmGrainIntensity = intensityFilmGrain;
        FilmGrainResponse = responseFilmGrain;
        SetPostProcessingSettings();
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

    private void SetPostProcessingSettings()
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
        return math.remap(value, from.x, from.y, to.x, to.y);
    }
}
