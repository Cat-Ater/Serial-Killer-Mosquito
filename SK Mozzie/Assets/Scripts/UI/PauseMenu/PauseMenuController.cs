using System.Collections;
using UnityEngine;

namespace UI
{
    public class PauseMenuController : MonoBehaviour
    {
        private const float TIME_DELAY = 0.05F;
        bool canSwitch = true;
        public GameObject menuRoot;

        private bool OpenState
        {
            get => menuRoot.activeSelf;
            set => menuRoot.SetActive(value);
        }

        void Start()
        {
            OpenState = false;
            menuRoot.SetActive(OpenState);
        }

        public void TooggleState()
        {
            if (!canSwitch)
                return;
            OpenState = !OpenState;
            Debug.Log("Openstate: " + OpenState);
            if (OpenState)
            {
                Time.timeScale = 0.1f;
                GameManager.Instance.DisablePlayer = PlayerState.DISABLED;
            }
            else
            {
                Time.timeScale = 1;
                GameManager.Instance.DisablePlayer = PlayerState.ENABLED; 
            }

            menuRoot.SetActive(OpenState);
            canSwitch = false;
            StartCoroutine(TimerDelay());
        }

        IEnumerator TimerDelay()
        {
            yield return new WaitForSeconds(TIME_DELAY);
            canSwitch = true;
        }
    }


}
