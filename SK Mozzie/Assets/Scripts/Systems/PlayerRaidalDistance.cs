using UnityEngine;

[System.Serializable]
public class PlayerRaidalDistance
{
    public Transform centerPos; 
    public float radius;
    public bool outsideRadius;
    public float maxTimeOutside = 0.55F;
    public float timeOutside = 0; 

    public float Intensity => 100 / maxTimeOutside;

    public bool OutsideRadius(Vector3 position)
    {
        Vector3 centre = centerPos.position;
        Vector3 player = position;
        Vector3 distanceVec = centre - player;
        Vector3 unitVec = distanceVec.normalized;
        if( distanceVec.magnitude >= (radius * unitVec).magnitude)
        {
            return true; 
        }

        return false;
    }

    public bool UpdateRadius(Vector3 position)
    {
        outsideRadius = OutsideRadius(position);

        if (outsideRadius)
        {
            timeOutside += Time.deltaTime;

            //Update shader
            GameManager.Instance.attackVisualisation.SetColor(ColorType.FAILURE);
            GameManager.Instance.attackVisualisation.VignetteIntensity = Intensity * timeOutside;
            GameManager.Instance.attackVisualisation.SetPostProcessingSettings();

            if (timeOutside >= maxTimeOutside)
                return true;
        }

        if (timeOutside > 0 && !outsideRadius)
        {
            timeOutside = 0;
            GameManager.Instance.attackVisualisation.VignetteIntensity = 0;
            GameManager.Instance.attackVisualisation.SetPostProcessingSettings();
        }

        return false;
    }
}
