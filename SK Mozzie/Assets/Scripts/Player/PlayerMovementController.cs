using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller used for managing the players movement. 
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    public Camera thirdPerson;
    public AxialRotClamp clampX;
    public AxialRotClamp clampY;
    public Rigidbody body;
    public AudioInputController audioInputController;
    public Vector3 cameraOffset; 
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
        UpdateCamera();
    }

    private void SetUpRotation()
    {
        clampX = new AxialRotClamp() { Min = -360, Max = 360, Current = 0 };
        clampY = new AxialRotClamp() { Min = -60, Max = 60, Current = 0 };
    }

    public void LUpdate()
    {
        UpdateMovement();
        UpdateRotation();
        UpdateCamera();
    }

    internal void UpdateVolume()
    {
        currentVolume = audioInputController.Average;
    }

    private void UpdateMovement()
    {
        if (PlayerController.state == PlayerState.DISABLED)
            return;

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
        if (PlayerController.state == PlayerState.DISABLED)
            return;

        clampX.UpdateLooped(Input.GetAxis("Mouse X") * clampX.turnSpeed);
        clampY.UpdateClamp(-Input.GetAxis("Mouse Y") * clampY.turnSpeed);

        //Update rotation and camera positions. 
        gameObject.transform.rotation = Quaternion.identity * Quaternion.Euler(clampY.Current, clampX.Current, 0);
    }

    private void UpdateCamera()
    {
        if (PlayerController.state == PlayerState.DISABLED)
            return;

        clampX.UpdateLooped(Input.GetAxis("Mouse X") * clampX.turnSpeed);
        clampY.UpdateClamp(-Input.GetAxis("Mouse Y") * clampY.turnSpeed);

        //thirdPerson.transform.position = transform.position + cameraOffset;
        thirdPerson.transform.rotation = transform.rotation;
    }
}
