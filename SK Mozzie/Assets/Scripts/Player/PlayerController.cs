using System.Collections;
using UnityEngine;

public enum PlayerState
{
    ENABLED, 
    DISABLED
}

public class PlayerController : MonoBehaviour
{
    public PlayerMovementController playerMovement;
    public PlayerState state = PlayerState.ENABLED;

    public PlayerState SetPlayerState
    {
        set
        {
            state = value; 
            if(state == PlayerState.DISABLED)
            {
                playerMovement.CancelMovement();
            }
        }
    }

    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value; 
    }

    void Start()
    {
        GameManager.Instance.playerC = this; 
    }

    void Update()
    {
        if(state == PlayerState.ENABLED)
        {
            playerMovement.UpdateVolume();
        }
    }

    private void LateUpdate()
    {
        if(state == PlayerState.ENABLED)
        {
            playerMovement.LUpdate();
        }
    }
}