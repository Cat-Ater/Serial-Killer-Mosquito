using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupLoader : MonoBehaviour
{

    public GameObject gamemangerPrefab; 

    // Start is called before the first frame update
    void Start()
    {
        //Generate new GameManager. 
        GameObject.Instantiate(gamemangerPrefab);
        StartCoroutine(TransitionTimer());
    }

    private IEnumerator TransitionTimer()
    {
        yield return new WaitForSeconds(0.55F);
        GameManager.Instance.LoadLevel("MainMenu");
    }
}
