using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameSFXPlayPoint
{
    NONE,
    START,
    END,
    TRIGGER_ENTER,
    TRIGGER_EXIT,
    COLLISION_ENTER,
    COLLISION_EXIT,
    TRIGGER_ENTER_PLAYER,
    TRIGGER_EXIT_PLAYER,
    COLLISION_ENTER_PLAYER,
    COLLISION_EXIT_PLAYER,
}

[System.Serializable]
public class SFX_Data
{
    [Header("When the audio should be played.")]
    public GameSFXPlayPoint PlayWhen;
    [Header("The minimum distance at which the audio should be played.")]
    public float minDist = 1;
    [Header("The maximum distance at which the audio should be played.")]
    public float maxDist = 400;
    [Header("The minimum volume at which the audio should be played.")]
    public float minVol = 0;
    [Header("The maximum volume at which the audio should be played.")]
    public float maxVol = 1;

    public bool IsPlayerBound
    {
        get
        {
            switch (PlayWhen)
            {
                case GameSFXPlayPoint.NONE:
                    return false;
                case GameSFXPlayPoint.START:
                    return false;
                case GameSFXPlayPoint.END:
                    return false;
                case GameSFXPlayPoint.TRIGGER_ENTER:
                    return false;
                case GameSFXPlayPoint.TRIGGER_EXIT:
                    return false;
                case GameSFXPlayPoint.COLLISION_ENTER:
                    return false;
                case GameSFXPlayPoint.COLLISION_EXIT:
                    return false;
                case GameSFXPlayPoint.TRIGGER_ENTER_PLAYER:
                    return true;
                case GameSFXPlayPoint.TRIGGER_EXIT_PLAYER:
                    return true;
                case GameSFXPlayPoint.COLLISION_ENTER_PLAYER:
                    return true;
                case GameSFXPlayPoint.COLLISION_EXIT_PLAYER:
                    return true;
                default:
                    return false;
            }
        }
    }
}

public class Game_SFX : MonoBehaviour
{
    public SFX_Data data;
    public AudioClip clip;

    private bool IsPlayer(GameObject obj)
    {
        if (obj.gameObject.tag != "Player")
            return true;
        return false;
    }

    public void Start()
    {
        if (data.PlayWhen == GameSFXPlayPoint.START)
            PlaySound();
    }

    public void OnDisable()
    {
        if (data.PlayWhen == GameSFXPlayPoint.END)
            PlaySound();
    }

    public void OnTriggerEnter(Collider other)
    {
        if ((IsPlayer(other.gameObject) && data.PlayWhen == GameSFXPlayPoint.TRIGGER_ENTER_PLAYER) ||
            (data.PlayWhen == GameSFXPlayPoint.TRIGGER_ENTER))
            PlaySound();
    }

    public void OnTriggerExit(Collider other)
    {
        if ((IsPlayer(other.gameObject) && data.PlayWhen == GameSFXPlayPoint.TRIGGER_EXIT_PLAYER) ||
            (data.PlayWhen == GameSFXPlayPoint.TRIGGER_EXIT))
            PlaySound();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if ((IsPlayer(collision.gameObject) && data.PlayWhen == GameSFXPlayPoint.COLLISION_ENTER_PLAYER) ||
            (data.PlayWhen == GameSFXPlayPoint.COLLISION_ENTER))
            PlaySound();
    }

    public void OnCollisionExit(Collision collision)
    {
        if ((IsPlayer(collision.gameObject) && data.PlayWhen == GameSFXPlayPoint.COLLISION_EXIT_PLAYER) ||
            (data.PlayWhen == GameSFXPlayPoint.COLLISION_EXIT))
            PlaySound();
    }

    public void PlaySound()
    {
        GameManager.Instance.PlaySoundAt(gameObject.transform.position, clip, data);
        gameObject.SetActive(false);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, data.maxDist);
    }
}
