using System;
using System.Collections;
using UnityEngine;

public class UI_TargetDisplay : MonoBehaviour
{
    string targetName; // The name of the target. 
    int targetIndex; // The index of the target (Imagine this as the target number in a list). 
    int currentHealth; // The current health of the player. 
    int healthMax; //The maximum health of the player.

    #region  Base System stuff. 
    internal void InitData(TargetData data)
    {
        targetName = name;
        targetIndex = data.Index;
        currentHealth = data.HealthCurrent;
        healthMax = data.HealthMax;
        OnIdle();
    }

    internal void UpdateData(TargetData data, bool idle, bool attacked, bool dead)
    {
        currentHealth = data.HealthCurrent;
        healthMax = data.HealthMax;
        UpdateUI();
        if (idle) OnIdle();
        if (attacked) OnAttack();
        if (dead) OnDeath(); 
    }
    #endregion

    internal void OnActivation()
    {
        //Do UI stuff here. 
    }

    internal void UpdateUI()
    {
        //Do general UI updates here. 
    }

    public void OnIdle()
    {
        //Do any updates related to Idle UI stuff here. 
    }

    public void OnAttack()
    {
        //Do any updates related to Attack UI stuff here.
    }

    public void OnDeath()
    {
        //Do any updates related to Death UI stuff here.
    }
}