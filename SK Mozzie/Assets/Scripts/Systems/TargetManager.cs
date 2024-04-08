using System.Collections.Generic;
using UnityEngine;

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
        if (index >= targets.Count)
        {
            Debug.Log("Game Completed");
            //Resolve game complete. 
            GameManager.Instance.LoadLevel("GameOver");
        }
        else
        {
            targets[index].SetActive();
        }
    }
}
