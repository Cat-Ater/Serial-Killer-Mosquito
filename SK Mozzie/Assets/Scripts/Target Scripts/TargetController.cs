using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TargetData))]
[RequireComponent(typeof(Collider))]
public class TargetController : MonoBehaviour
{
    public TargetData targetData;
    public bool active = false;

    public void SetActive()
    {
        active = true; 
    }

    public void Update()
    {
        if (targetData.IsDead)
        {
            active = false; 
            GameManager.Instance.SetNextTarget();
        }
    }

    public CurrentTargetState GetState()
    {
        return new CurrentTargetState(targetData.playIdleAnim, targetData.playAttackAnim, targetData.playDeathAnim);
    }
}