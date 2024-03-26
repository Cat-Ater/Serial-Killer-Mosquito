using System.Collections;
using System.Collections.Generic;
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

[System.Serializable]
public class KeyPressMonitor
{
    public KeyCode key;
    public bool active = false;
    public float heldTime = 0;
    public float maxHeldTime = 0;

    public void Update()
    {
        if (Input.GetKey(key) && !active)
        {
            active = true;
        }
        else if (Input.GetKey(key) == false && active)
        {
            active = false;
            heldTime = 0;
        }

        if (active && heldTime < maxHeldTime)
        {
            heldTime = Mathf.Clamp(heldTime + Time.deltaTime, 0, maxHeldTime);
        }
    }
}

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    public AxialRotClamp clampX;
    public AxialRotClamp clampY;
    public Rigidbody body;
    public AudioInputController audioInputController;
    public float hoverMaxSpeed;
    public float fwdHoverMomentumMax = 0.2F;
    public bool hovering = false;
    public Transform player;

    public float currentVolume;

#if DEBUG
    private bool InputActive => (Input.GetKey(KeyCode.Space));
#endif

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        //audioInputController.MicrophoneToAudioClip();
        Camera.main.transform.rotation = transform.rotation;
        SetUpRotation();
    }

    private void SetUpRotation()
    {
        clampX = new AxialRotClamp() { Min = -360, Max = 360, Current = 0 };
        clampY = new AxialRotClamp() { Min = -30, Max = 30, Current = 0 };
    }

    private void Update()
    {
        currentVolume = audioInputController.Average;
    }

    private void LateUpdate()
    {
        clampX.UpdateLooped(Input.GetAxis("Mouse X") * clampX.turnSpeed);
        clampY.UpdateClamp(-Input.GetAxis("Mouse Y") * clampY.turnSpeed);

        UpdateMovement();
        UpdateRotation();
        UpdateCamera();

    }

    private void UpdateMovement()
    {
        float fwdSpeed = fwdHoverMomentumMax * Time.deltaTime;
        float upwardSpeed = hoverMaxSpeed * Time.deltaTime;
        Vector3 fwdVec = gameObject.transform.forward.normalized;

#if DEBUG
        if (InputActive)
            currentVolume = 1;
#endif

        if (currentVolume > 0)
            body.velocity = new Vector3(fwdVec.x * fwdSpeed, fwdVec.y * upwardSpeed, fwdVec.z * fwdSpeed);
        if (currentVolume <= 0)
            body.velocity = ReduceVelocity(4);

    }

    private Vector3 ReduceVelocity(int rate)
    {
        Vector3 v = body.velocity / rate;
        v.x = (v.x < 0) ? 0 : v.x;
        v.z = (v.z < 0) ? 0 : v.z;
        return v;
    }

    private void UpdateRotation()
    {
        //Update rotation and camera positions. 
        gameObject.transform.rotation = Quaternion.identity * Quaternion.Euler(clampY.Current, clampX.Current, 0);
    }

    private void UpdateCamera()
    {
        Camera.main.transform.position = transform.position;
        Camera.main.transform.rotation = transform.rotation;
    }
}
