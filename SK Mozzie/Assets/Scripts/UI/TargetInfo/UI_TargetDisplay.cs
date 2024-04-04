using System.Collections;
using UnityEngine;

public enum UITargetState
{
    NO_TARGET, 
    TARGET_SET, 
    TARGET_RESOLUTION
}

public class UI_TargetDisplay : MonoBehaviour
{
    public TargetData data;
    public UITargetState targetState = UITargetState.NO_TARGET;

    void Start()
    {

    }

    void Update()
    {

    }
}