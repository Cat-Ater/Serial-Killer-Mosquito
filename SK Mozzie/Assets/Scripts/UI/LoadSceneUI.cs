using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneUI : MonoBehaviour
{
    public void LoadMainScene()
    {
        GameManager.Instance.LoadLevel("Main_Scene");
    }
}
