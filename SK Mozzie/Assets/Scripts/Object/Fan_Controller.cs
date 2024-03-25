using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates fan knockback on hit. 
/// </summary>
public class Fan_Controller : MonoBehaviour
{
    public float pushStrength;
    public bool isPushing = false;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isPushing)
        {
            Rigidbody body = other.GetComponent<Rigidbody>();
            StartCoroutine(PushEffect(body, pushStrength, 20));
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isPushing)
        {
            Rigidbody body = other.GetComponent<Rigidbody>();
            StartCoroutine(PushEffect(body, pushStrength, 20));
        }
    }

    private IEnumerator PushEffect(Rigidbody body, float force, int frames)
    {
        //Inital definitions. 
        int currentFrames = 0; 
        Vector2 direction = body.transform.position - gameObject.transform.position;
        direction.Normalize();

        isPushing = true;

        while(currentFrames < frames)
        {
            body.AddForce(force * direction);
            yield return new WaitForEndOfFrame();
            currentFrames++; 
        }
        isPushing = false;
    }
}
