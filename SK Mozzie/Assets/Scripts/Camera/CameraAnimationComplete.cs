using UnityEngine;

public class CameraAnimationComplete : MonoBehaviour
{
    public string cameraToSwitchTo = "Main";
    public bool switchCamera = false; 

    public void Update()
    {
        if(switchCamera == true)
        {
            SwitchTo();
        }
    }

    public void SwitchTo()
    {
        GameManager.Instance.SwitchCamera(cameraToSwitchTo);
        gameObject.SetActive(false);
    }
}