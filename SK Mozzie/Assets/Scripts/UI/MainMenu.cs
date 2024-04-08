using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for handling the main menu. 
/// </summary>
public class MainMenu : MonoBehaviour
{
    private enum MenuState
    {
        INACTIVE,
        ACTIVE,
        SETTINGS_ACTIVE
    }

    private MenuState menuState = MenuState.INACTIVE;
    public GameObject menuRoot;
    public SettingsMenu settingsMenu;

    public AudioClip openMenuSound; 
    public AudioClip exitMenuSound;

    public bool Open
    {
        get => menuRoot.activeSelf;
        private set => menuRoot.SetActive(value);
    }

    public void PlayIntro()
    {
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
        Cursor.lockState = CursorLockMode.None;
    }

    public void SettingsMenuOpen()
    {
        settingsMenu.PopulateData();

        //Disable the main. 
        Open = !(settingsMenu.Open = true);
        menuState = MenuState.SETTINGS_ACTIVE;
        GameManager.Instance.PlayUISFX(openMenuSound);
    }

    public void SettingsMenuClose()
    {
        settingsMenu.PushData();

        //Enable the main. 
        Open = !(settingsMenu.Open = false);
        menuState = MenuState.ACTIVE;
        GameManager.Instance.PlayUISFX(exitMenuSound);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") SceneManager.LoadScene(3);
        Cursor.lockState = CursorLockMode.Confined;
    }
}

