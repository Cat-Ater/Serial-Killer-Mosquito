using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMosquitoAttack
{
    public void OnDrainInterupt();
    public void OnDrainComplete();
}

public enum TargetHealthState
{
    ALIVE,
    ATTACKED,
    DEAD
}

[System.Serializable]
public class PostProssScaling
{
    [Range(0,100)]
    public float maxValue;
    [HideInInspector] public float timeTotal;
    public float timeScale;

    public float Speed => maxValue / timeScale;
    public float Value => Mathf.Clamp(timeTotal * Speed, 0, maxValue);
}

public class TargetHealth : MonoBehaviour
{
    public IMosquitoAttack attack;
    public TargetHealthState state = TargetHealthState.ALIVE;
    public PostProssScaling vIntensity;
    public PostProssScaling vSmoothing;
    public PostProssScaling fgIntensity; 
    public PostProssScaling fgResponse;

    private bool CanActivate => state == TargetHealthState.ALIVE;

    public void OnDrainActivate(IMosquitoAttack attack)
    {
        if (CanActivate)
        {
            state = TargetHealthState.ATTACKED;
            this.attack = attack;
        }
    }

    public void OnDrainCancel()
    {
        if (state != TargetHealthState.DEAD)
            state = TargetHealthState.ALIVE;
        this.attack = null;
        //Update the shader. 
        GameManager.Instance.attackVisualisation.VignetteIntensity = 0;
        GameManager.Instance.attackVisualisation.FilmGrainIntensity = 0;
        GameManager.Instance.attackVisualisation.VignetteSmoothness = 0;
        GameManager.Instance.attackVisualisation.FilmGrainResponse = 0;
        GameManager.Instance.attackVisualisation.SetPostProcessingSettings();
        Debug.Log("Drain deactivated!");
    }

    public void UpdateData(ref TargetDataStruct data)
    {
        bool isDrained = data.healthCurrent <= 0;

        bool continueDrain = state == TargetHealthState.ATTACKED && !isDrained;

        //Update timings. 
        vIntensity.timeTotal += Time.deltaTime; 
        fgIntensity.timeTotal += Time.deltaTime; 
        vSmoothing.timeTotal += Time.deltaTime; 
        fgResponse.timeTotal += Time.deltaTime; 

        //Update the shader. 
        GameManager.Instance.attackVisualisation.VignetteIntensity = vIntensity.Value; 
        GameManager.Instance.attackVisualisation.FilmGrainIntensity = fgIntensity.Value; 
        GameManager.Instance.attackVisualisation.VignetteSmoothness = vSmoothing.Value;
        GameManager.Instance.attackVisualisation.FilmGrainResponse = fgResponse.Value;
        GameManager.Instance.attackVisualisation.SetPostProcessingSettings();


        if (continueDrain)
        {
            data.healthCurrent -= data.rateOfExtraction;
        }

        //Check if drain is completed.
        if (isDrained)
        {
            state = TargetHealthState.DEAD;
        }
    }
}
