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
    REPEATED_INTERVAL_COLLISION_ENTER,
    REPEATED_INTERVAL_COLLISION_EXIT,
    REPEATED_INTERVAL_TRIGGER_ENTER,
    REPEATED_INTERVAL_TRIGGER_EXIT,
    INTERVAL
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
    public bool playLooped = false;

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
    public float intervalTime;
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

        if (data.PlayWhen == GameSFXPlayPoint.INTERVAL)
            StartCoroutine(IntervalTimer());
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

        if ((IsPlayer(other.gameObject) && data.PlayWhen == GameSFXPlayPoint.REPEATED_INTERVAL_TRIGGER_ENTER))
        {
            StartCoroutine(IntervalTimer());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if ((IsPlayer(other.gameObject) && data.PlayWhen == GameSFXPlayPoint.TRIGGER_EXIT_PLAYER) ||
            (data.PlayWhen == GameSFXPlayPoint.TRIGGER_EXIT))
            PlaySound();

        if ((IsPlayer(other.gameObject) && data.PlayWhen == GameSFXPlayPoint.REPEATED_INTERVAL_TRIGGER_EXIT))
        {
            StartCoroutine(IntervalTimer());
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if ((IsPlayer(collision.gameObject) && data.PlayWhen == GameSFXPlayPoint.COLLISION_ENTER_PLAYER) ||
            (data.PlayWhen == GameSFXPlayPoint.COLLISION_ENTER))
            PlaySound();

        if ((IsPlayer(collision.gameObject) && data.PlayWhen == GameSFXPlayPoint.REPEATED_INTERVAL_COLLISION_ENTER))
        {
            StartCoroutine(IntervalTimer());
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if ((IsPlayer(collision.gameObject) && data.PlayWhen == GameSFXPlayPoint.COLLISION_EXIT_PLAYER) ||
            (data.PlayWhen == GameSFXPlayPoint.COLLISION_EXIT))
            PlaySound();

        if ((IsPlayer(collision.gameObject) && data.PlayWhen == GameSFXPlayPoint.REPEATED_INTERVAL_COLLISION_EXIT))
        {
            StartCoroutine(IntervalTimer());
        }
    }

    private IEnumerator IntervalTimer()
    {
        GameManager.Instance.PlaySoundFXAt(gameObject.transform.position, clip, data);
        yield return new WaitForSeconds(intervalTime);
        StartCoroutine(IntervalTimer());
    }

    public void PlaySound()
    {
        GameManager.Instance.PlaySoundFXAt(gameObject.transform.position, clip, data);
        this.enabled = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, data.maxDist);
    }
}
