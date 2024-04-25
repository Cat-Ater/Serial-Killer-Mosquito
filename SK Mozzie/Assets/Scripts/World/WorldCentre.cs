using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCentre : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.playerRaidusWorld.centerPos = this.transform;
    }
}
