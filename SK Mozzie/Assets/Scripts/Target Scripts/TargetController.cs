using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TargetData))]
[RequireComponent(typeof(Collider))]
public class TargetController : MonoBehaviour
{
    public TargetData targetData;
    public bool active = false;

    public void Start()
    {
        GameManager.Instance.Targets.Add(this);
    }

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
}