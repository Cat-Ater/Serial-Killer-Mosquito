using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AxialRotClamp
{
    public float Min { get; set; }
    public float Max { get; set; }
    public float Current { get; set; }

    public void Update(float dt)
    {
        Current += dt;

        if (Current > Max)
            Current = Min;
        if (Current < Min)
            Current = Max;
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
    public KeyPressMonitor keyMonitor;
    public float turnSpeed = 1.0F;
    public float hoverMaxSpeed;
    public float fwdHoverMomentumMax = 0.2F;
    public bool hovering = false;
    public Transform player;
    public float xRot = 0;
    public float yRot = 0; 

    private Vector3 offset;

    public KeyCode upKey = KeyCode.Space;

    private float GetHoverStep => hoverMaxSpeed / keyMonitor.maxHeldTime;
    private float GetFWDStep => fwdHoverMomentumMax / keyMonitor.maxHeldTime;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        clampX = new AxialRotClamp() { Min = -360, Max = 360, Current = 0 };
        clampY = new AxialRotClamp() { Min = -30, Max = 30, Current = 0 };
        offset = new Vector3(0, 2.5f, -5);
        Camera.main.transform.rotation = transform.rotation;
    }

    private void Update()
    {
        keyMonitor.Update();
        if (keyMonitor.active == false)
        {
            body.velocity = Vector3.zero;
            hovering = false;
        }


    }

    private void LateUpdate()
    {
        float deltaX = Input.GetAxis("Mouse X") * turnSpeed;
        float deltaY = Input.GetAxis("Mouse Y") * turnSpeed;
        float fwdSpeed = Mathf.Clamp(keyMonitor.heldTime, 0, fwdHoverMomentumMax) * GetFWDStep;
        float hoverSpeed = Mathf.Clamp(keyMonitor.heldTime, 0, hoverMaxSpeed) * GetHoverStep;
        clampX.Update(deltaX);
        clampY.Update(-deltaY);

        Vector3 fwdVec = gameObject.transform.forward.normalized;

        body.velocity = new Vector3(0, fwdVec.y * hoverSpeed, fwdVec.z * fwdSpeed);
        Camera.main.transform.position = transform.position;

        gameObject.transform.rotation = Quaternion.identity * Quaternion.Euler(clampY.Current, clampX.Current, 0);
        //transform.RotateAround(transform.position, Vector3.right, deltaY);
        //transform.RotateAround(transform.position, Vector3.up, deltaX);
        Camera.main.transform.rotation = transform.rotation;
    }
}
