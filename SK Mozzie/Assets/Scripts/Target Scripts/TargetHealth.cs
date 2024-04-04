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
    public TargetHealthState state = TargetHealthState.ALIVE;

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
        if (state == TargetHealthState.DEAD)
            state = TargetHealthState.ALIVE;
        this.attack = null;
        Debug.Log("Drain deactivated!");
    }

    public void UpdateData(ref TargetDataStruct data)
    {
        bool isDrained = data.healthCurrent <= 0;
        bool continueDrain = state == TargetHealthState.ATTACKED && !isDrained;

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
