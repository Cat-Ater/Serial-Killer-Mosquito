using System.Collections;
using UnityEngine;

[System.Serializable]
public struct TargetDataStruct
{
    [SerializeField] public string name;
    [SerializeField] public int index;
    [SerializeField] public float healthCurrent;
    [SerializeField] public float healthMax;
    [SerializeField] public float rateOfExtraction;
    [SerializeField] public string killCompletionLine;
}

[System.Serializable]
[RequireComponent(typeof(TargetHealth))]
public class TargetData : MonoBehaviour
{
    public TargetHealth targetHealth;
    public TargetDataStruct tData;
    public TargetController targetController;

    public string TargetName => tData.name;
    public int Index => tData.index;
    public float HealthMax => tData.healthMax;
    public float HealthCurrent
    {
        get => tData.healthCurrent;
        set
        {
            tData.healthCurrent = Mathf.Clamp(tData.healthCurrent + value, 0, HealthMax);
        }
    }
    public float RateOfExtraction => tData.rateOfExtraction;

    [HideInInspector] public bool playIdleAnim = true;
    [HideInInspector] public bool playAttackAnim = false;
    [HideInInspector] public bool playDeathAnim = false;
    [HideInInspector] public bool deathAnimComplete = false;

    public float timeTillDead;
    public bool IsDead => tData.healthCurrent <= 0;
    public void OnValidate()
    {
        timeTillDead = (tData.healthMax / RateOfExtraction);
    }

    public void OnEnable()
    {

    }

    public void Update()
    {
        if (targetController.active)
        {
            if (targetHealth.state == TargetHealthState.ATTACKED)
            {
                targetHealth.UpdateData(ref tData);
            }
            ResolveTargetState();
            UIManager.Instance.SetTargetData(this, playIdleAnim, playAttackAnim, playDeathAnim);
        }
    }

    private void ResolveTargetState()
    {
        if (playDeathAnim)
            return;

        switch (targetHealth.state)
        {
            case TargetHealthState.ALIVE:
                Animation_Idle();
                break;
            case TargetHealthState.ATTACKED:
                Animation_Attacked();
                break;
            case TargetHealthState.DEAD:
                Animation_Dead();
                break;
        }
    }

    public void Animation_Idle()
    {
        playIdleAnim = true;
        playDeathAnim = playAttackAnim = false;
    }

    public void Animation_Attacked()
    {
        playAttackAnim = true;
        playDeathAnim = playIdleAnim = false;
    }

    public void Animation_Dead()
    {
        playDeathAnim = true;
        playAttackAnim = playIdleAnim = false;
    }

    public void OnDeathAnimCompleted()
    {
        //Do some logic updating here. 
        //Set gameobject inactive. 
    }
}