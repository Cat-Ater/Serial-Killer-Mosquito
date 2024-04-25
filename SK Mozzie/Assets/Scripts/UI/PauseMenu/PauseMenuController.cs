using System.Collections;
using UnityEngine;

namespace UI
{

    public class PauseMenuController : MonoBehaviour
    {
        private enum MenuState
        {
            INACTIVE,
            ACTIVE,
            SETTINGS_ACTIVE
        }

        private MenuState menuState = MenuState.INACTIVE;
        private const float TIME_DELAY = 0.05F;
        bool canSwitch = true;
        public GameObject menuRoot;
        public SettingsMenu settingsMenu;

        public bool Open
        {
            get => menuRoot.activeSelf;
            private set => menuRoot.SetActive(value);
        }

        void Start()
        {
            Open = false;
            menuRoot.SetActive(Open);
        }

        private void Update()
        {
            switch (menuState)
            {
                case MenuState.INACTIVE:
                    State_Inactive();
                    break;
                case MenuState.ACTIVE:
                    State_Active();
                    break;
                case MenuState.SETTINGS_ACTIVE:
                    State_Settings();
                    break;
            }
        }

        IEnumerator TimerDelay()
        {
            yield return new WaitForSeconds(TIME_DELAY);
            canSwitch = true;
        }

        public void State_Inactive()
        {
            if (canSwitch && Input.GetKey(KeyCode.Escape))
                UpdateMenuState(MenuState.ACTIVE);
        }

        private void UpdateMenuState(MenuState state)
        {

            //Update open state.
            Open = !Open;

            //Update the menu state. 
            menuState = state;

            //Start Delay. 
            canSwitch = false;
            StartCoroutine(TimerDelay());
        }

        public void State_Active()
        {
            GameManager.Instance.PlayerMovement = PlayerState.DISABLED; 
        }

        public void State_Settings()
        {

        }

        public void SettingsMenuOpen()
        {
            settingsMenu.PopulateData();

            //Disable the main. 
            Open = !(settingsMenu.Open = true);
            menuState = MenuState.SETTINGS_ACTIVE;
        }

        public void SettingsMenuClose()
        {
            settingsMenu.PushData();

            //Enable the main. 
            Open = !(settingsMenu.Open = false);
            menuState = MenuState.ACTIVE;
        }

        public void MenuClose()
        {
            UpdateMenuState(MenuState.INACTIVE);
            GameManager.Instance.PlayerMovement = PlayerState.ENABLED;
        }

        public void ReturnToMain()
        {
            GameManager.Instance.LoadMainMenu();
        }
    }
}