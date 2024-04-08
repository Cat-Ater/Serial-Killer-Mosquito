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
    public static PlayerState state = PlayerState.ENABLED;

    public PlayerState SetPlayerState
    {
        set => state = value;
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
        playerMovement.UpdateVolume();
    }

    private void LateUpdate()
    {
        playerMovement.LUpdate();
    }
}