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
            th = other.gameObject.GetComponent<TargetHealth>();
            th.OnDrainActivate(this);
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
