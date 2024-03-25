using UnityEngine;

public class PlayerDistanceDetector : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player entered collision!");
        }
    }
}
