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

public class TargetHealth : MonoBehaviour
{
    public IMosquitoAttack attack;
    public TargetData data;
    public TargetHealthState state = TargetHealthState.ALIVE; 

    public bool canBeDrained = true;

    private bool IsDrained => data.HealthCurrent <= 0 && state == TargetHealthState.ATTACKED;
    private bool ContinueDrain => state == TargetHealthState.ATTACKED && !IsDrained;

    public void OnDrainActivate(IMosquitoAttack attack)
    {
        if (!(state == TargetHealthState.ALIVE && canBeDrained))
            return;

        state = TargetHealthState.ATTACKED;
        this.attack = attack;
    }

    public void UpdateData()
    {
        if (ContinueDrain)
        {
            data.HealthCurrent -= data.RateOfExtraction;

            //Check if drain is completed.
            if (IsDrained)
            {
                state = TargetHealthState.DEAD;
            }
        }
    }
}
