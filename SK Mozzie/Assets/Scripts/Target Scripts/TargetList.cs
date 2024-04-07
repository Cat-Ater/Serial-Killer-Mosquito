using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetList : MonoBehaviour
{
    public List<TargetController> targets; 

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Targets = targets; 
    }
}
