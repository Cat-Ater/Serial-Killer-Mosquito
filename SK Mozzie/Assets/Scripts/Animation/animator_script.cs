using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetAnimatorController : MonoBehaviour
{
    public TargetData targetData;
    public Animator animator;

    private void Start()
    {
        // Make sure targetData and animator are assigned
        if (targetData == null || animator == null)
        {
            Debug.LogError("TargetData or Animator is not assigned in TargetAnimatorController.");
            return;
        }
    }

    private void Update()
    {
        // Check the state of the target and set animator bools accordingly
        switch (targetData.targetHealth.state)
        {
            case TargetHealthState.ALIVE:
                SetAnimatorBools(targetData.playIdleAnim, false, false);
                break;
            case TargetHealthState.ATTACKED:
                SetAnimatorBools(false, targetData.playAttackAnim, false);
                break;
            case TargetHealthState.DEAD:
                SetAnimatorBools(false, false, targetData.playDeathAnim);
                break;
        }
    }

    // Method to set animator bools
    private void SetAnimatorBools(bool idle, bool attacked, bool dead)
    {
        animator.SetBool("Idle", idle);
        animator.SetBool("Attacked", attacked);
        animator.SetBool("Dead", dead);
    }
}

