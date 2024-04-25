using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Death")
        {
            GameManager.Instance.LoadLevel("MainMenu");
        }
    }
}
