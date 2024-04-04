using System.Collections;
using UnityEngine;

public class TargetData : MonoBehaviour
{
    TargetHealth tH;
    [SerializeField] string targetName;
    [SerializeField] int targetIndex;
    [SerializeField] int healthCurrent;
    [SerializeField] int healthMax;
    [SerializeField] int rateOfExtraction;

    public string TargetName => targetName;
    public int Index => Index;
    public int HealthMax => healthMax;
    public int HealthCurrent
    {
        get => healthCurrent; 
        set
        {
            healthCurrent = Mathf.Clamp(value, 0, healthMax);
        }
    } 
    public int RateOfExtraction => rateOfExtraction;
    public bool playIdleAnim = true; 
    public bool playAttackAnim = true; 
    public bool playDeathAnim = true;
    public bool deathAnimComplete = false; 

    public void OnEnable()
    {
        tH = gameObject.AddComponent<TargetHealth>();
        tH.data = this;
    }

    public void Update()
    {
        tH.UpdateData();
        ResolveTargetState(); 
    }

    private void ResolveTargetState()
    {
        switch (tH.state)
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
        if(playIdleAnim == false)
        {
            playIdleAnim = true;
            playDeathAnim = playAttackAnim = false; 
        }
    }

    public void Animation_Attacked()
    {
        if (playAttackAnim == false)
        {
            playAttackAnim = true;
            playDeathAnim = playIdleAnim = false;
        }
    }

    public void Animation_Dead()
    {
        if (playDeathAnim == false)
        {
            playDeathAnim = true;
            playIdleAnim = playIdleAnim = false;
        }
    }

    public void OnDeathAnimCompleted()
    {
        //Do some logic updating here. 
        //Set gameobject inactive. 
    }
}