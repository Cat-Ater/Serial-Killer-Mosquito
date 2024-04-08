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

    public TargetState GetState()
    {
        return new TargetState(targetData.playIdleAnim, targetData.playAttackAnim, targetData.playDeathAnim);
    }
}