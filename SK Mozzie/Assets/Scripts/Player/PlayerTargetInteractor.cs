using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetInteractor : MonoBehaviour, IMosquitoAttack
{
    TargetHealth th;
    public bool isSucking = false;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<TargetHealth>() != null && th == null)
        {
            Debug.Log("Player Attacking Target");
            th = other.gameObject.GetComponent<TargetHealth>();
            th.OnDrainActivate(this);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<TargetHealth>() != null && th != null)
        {
            if(other.gameObject.GetComponent<TargetHealth>() == th)
            th.OnDrainCancel();
            th = null;
            Debug.Log("Player Ceased Attacking Target");
        }
    }

    public void OnDrainComplete()
    {
        Debug.Log("Target drained!!!!");
    }

    public void OnDrainInterupt()
    {
        Debug.Log("Target drain interupted!!!!");
    }
}
