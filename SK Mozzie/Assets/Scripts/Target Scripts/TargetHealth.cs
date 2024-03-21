using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMosquitoAttack
{
    public void OnDrainInterupt();
    public void OnDrainComplete();
}

public class TargetHealth : MonoBehaviour
{
    public IMosquitoAttack attack;
    public int currentHealth;
    public int minHealth = 0;
    public int maxHealth = 100;
    public int drainPerSecond = 1;
    public bool isDraining = false;
    public bool canBeDrained = true;

    private bool IsDrained => currentHealth <= 0;  

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDraining && !IsDrained)
        {
            currentHealth -= drainPerSecond;

            if (currentHealth <= 0)
            {
                Debug.Log("Draining target completed.");
                attack.OnDrainComplete();
            }
        }
    }

    public void OnDrainActivate(IMosquitoAttack attack)
    {
        if (canBeDrained && !isDraining)
        {
            isDraining = true;
            this.attack = attack;
        }
    }
}
