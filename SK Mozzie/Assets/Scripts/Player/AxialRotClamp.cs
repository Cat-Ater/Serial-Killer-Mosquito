using UnityEngine;

public class AxialRotClamp
{
    public float turnSpeed = 6.3F;
    public float Min { get; set; }
    public float Max { get; set; }
    public float Current { get; set; }

    public void UpdateLooped(float dt)
    {
        Current += dt;

        if (Current > Max)
            Current = Min;
        if (Current < Min)
            Current = Max;
    }

    public void UpdateClamp(float dt)
    {
        Current = Mathf.Clamp(Current + dt, Min, Max);
    }
}
