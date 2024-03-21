using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AxialRotClamp
{
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
    public Rigidbody body;
    public AxialRotClamp clampX;
    public AxialRotClamp clampY;
    public AudioInputController audioInputController;
    public float turnSpeed = 1.0F;
    public float hoverMaxSpeed;
    public float fwdHoverMomentumMax = 0.2F;
    public bool hovering = false;
    public Transform player;
    public float xRot = 0;
    public float yRot = 0;

    public float currentVolume;
    public float volumeThreshold = 0.025F;
    public float outputScalar = 100; 

    private bool UpdateMovement => currentVolume >= volumeThreshold;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        audioInputController.MicrophoneToAudioClip();
        Camera.main.transform.rotation = transform.rotation;
        SetUpRotation();
    }

    private void SetUpRotation()
    {
        clampX = new AxialRotClamp() { Min = -360, Max = 360, Current = 0 };
        clampY = new AxialRotClamp() { Min = -30, Max = 30, Current = 0 };
    }

    int pos = 0;
    private void Update()
    {
        currentVolume = audioInputController.Average;
    }

    private void LateUpdate()
    {
        float deltaX = Input.GetAxis("Mouse X") * turnSpeed;
        float deltaY = Input.GetAxis("Mouse Y") * turnSpeed;
        clampX.UpdateLooped(deltaX);
        clampY.UpdateClamp(-deltaY);
        float fwdSpeed = 0;
        float hoverSpeed = 0;
        GetForwardMomentum(ref fwdSpeed, ref hoverSpeed);

        Vector3 fwdVec = gameObject.transform.forward.normalized;

        body.velocity = new Vector3(0, fwdVec.y * hoverSpeed, fwdVec.z * fwdSpeed);

        //Update rotation and camera positions. 
        gameObject.transform.rotation = Quaternion.identity * Quaternion.Euler(clampY.Current, clampX.Current, 0);
        UpdateCamera();
    }

    private void GetForwardMomentum(ref float fwdSpeed, ref float upwardSpeed)
    {
        fwdSpeed = currentVolume * outputScalar * fwdHoverMomentumMax * Time.deltaTime;
        upwardSpeed = currentVolume * outputScalar * hoverMaxSpeed * Time.deltaTime;

        Debug.Log("FWDM: " + fwdSpeed);
        Debug.Log("UWDM: " + upwardSpeed);
    }

    private void UpdateCamera()
    {
        Camera.main.transform.position = transform.position;
        Camera.main.transform.rotation = transform.rotation;
    }
}
