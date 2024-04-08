using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetManager
{
    public List<TargetController> targets;
    public int index = 0;
    public bool systemActive;
    public bool targetSelected;

    public bool CanDisplayTarget => systemActive && targetSelected;
    public bool TargetEnabled => targets[index].active;
    public TargetDataStruct TargetData => targets[index].targetData.tData;
    public TargetState CTargetState => targets[index].GetState();
    public bool AllDead => index >= targets.Count;
    public void Update()
    {
        if (CTargetState.dead == true)
            SelectNext();
    }

    public void SetTargets(List<TargetController> targetList)
    {
        this.targets = targetList;
    }

    public void InitalizeSystem()
    {
        if (targets != null)
        {
            index = 0;
            targets[index].SetActive();
            systemActive = true;
        }
        else
        {
            Debug.Log("No Target Data found.");
        }
    }

    public void SelectNext()
    {
        targets[index].active = false;
        index++;
        if (index < targets.Count)
        {
            targets[index].SetActive();
        }
    }

    public bool AllTargetsDead()
    {

        foreach (TargetController controller in targets)
        {
            if (controller.targetData.HealthCurrent > 0)
                return false;
        }

        return true;
    }
}
