using UnityEngine;

[System.Serializable]
public class Target_SFX_Data
{
    [Header("When the audio should be played.")]
    public TargetAudioEffect PlayWhen;
    [Header("The minimum distance at which the audio should be played.")]
    public float minDist = 1;
    [Header("The maximum distance at which the audio should be played.")]
    public float maxDist = 400;
    [Header("The minimum volume at which the audio should be played.")]
    public float minVol = 0;
    [Header("The maximum volume at which the audio should be played.")]
    public float maxVol = 1;
    public bool playLooped = false;

    public SFX_Data ToSFXData()
    {
        return new SFX_Data()
        {
            PlayWhen = GameSFXPlayPoint.NONE,
            minDist = this.minDist,
            maxDist = this.maxDist,
            minVol = this.minVol,
            maxVol = this.maxVol,
            playLooped = this.playLooped
        };
    }
}

public class Target_Game_SFX : MonoBehaviour
{
    public Target_SFX_Data data;
    public AudioClip clip;
    public TargetData targetData;


    public void Update()
    {
        if (targetData.HealthCurrent <= 0 && data.PlayWhen == TargetAudioEffect.TARGET_ON_DEATH)
        {
            GameManager.Instance.PlaySoundFXAt(transform.position, clip, data.ToSFXData());
        }
    }

}