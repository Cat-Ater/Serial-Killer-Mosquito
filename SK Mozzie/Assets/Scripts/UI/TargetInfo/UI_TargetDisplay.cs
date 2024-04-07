using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum UIElementState
{
    DISABLED,
    ALIVE,
    ATTACKED,
    DEAD
}

[System.Serializable]
public struct StateColors
{
    public Color alive;
    public Color attacked;
    public Color dead;
}

[System.Serializable]
public class UIElement
{
    private Color inactive = new Color(0F, 0F, 0F, 0F);
    private UIElementState state;
    public TextMeshProUGUI textOutput;
    public Image underlay;
    public StateColors stateColorsText;
    public StateColors stateColorsUnderlay;

    public string SetText
    {
        set => textOutput.text = value;
    }

    public void Setup() => SetState(UIElementState.DISABLED);

    public void SetState(UIElementState state)
    {
        if (this.state == state)
            return;

        switch (state)
        {
            case UIElementState.DISABLED:
                textOutput.color = inactive;
                underlay.color = inactive;
                textOutput.text = "";
                break;
            case UIElementState.ALIVE:
                underlay.color = stateColorsUnderlay.alive;
                textOutput.color = stateColorsText.alive;
                break;
            case UIElementState.ATTACKED:
                underlay.color = stateColorsUnderlay.attacked;
                textOutput.color = stateColorsText.attacked;
                break;
            case UIElementState.DEAD:
                underlay.color = stateColorsUnderlay.dead;
                textOutput.color = stateColorsText.dead;
                break;
            default:
                textOutput.color = inactive;
                underlay.color = inactive;
                textOutput.text = "";
                break;
        }
        this.state = state;
    }
}

namespace UI
{
    public class UI_TargetDisplay : MonoBehaviour
    {
        public TargetDataStruct data;

        string targetName; // The name of the target. 
        int targetIndex; // The index of the target (Imagine this as the target number in a list). 
        float currentHealth; // The current health of the player. 
        float healthMax; //The maximum health of the player.

        public GameObject rootObject;
        public UIElement nameOutput;
        public UIElement targetNumberOutput;
        public UIElement currentHealthOutput;
        public UIElement maxHealthOutput;

        private void OnEnable()
        {
            nameOutput.Setup();
            targetNumberOutput.Setup();
            currentHealthOutput.Setup();
            maxHealthOutput.Setup();
        }

        #region  Base System stuff. 
        internal void InitData(ref TargetDataStruct data)
        {
            this.data = data;
            OnIdle();
        }

        internal void UpdateData(bool idle, bool attacked, bool dead)
        {
            UpdateUI();
            if (idle)
            {
                OnIdle();
                return;
            }
            if (attacked)
            {
                OnAttack();
                return;
            }
            if (dead)
            {
                OnDeath();
                return;
            }
        }
        #endregion

        internal void UpdateUI()
        {
            //Do general UI updates here. 
            nameOutput.SetText = data.name;
            targetNumberOutput.SetText = data.index.ToString();
            currentHealthOutput.SetText = data.healthCurrent.ToString();
            maxHealthOutput.SetText = data.healthMax.ToString();
        }

        public void OnIdle()
        {
            //Do any updates related to Idle UI stuff here.
            rootObject.SetActive(true);
            nameOutput.SetState(UIElementState.ALIVE);
            targetNumberOutput.SetState(UIElementState.ALIVE);
            currentHealthOutput.SetState(UIElementState.ALIVE);
            maxHealthOutput.SetState(UIElementState.ALIVE);
        }

        public void OnAttack()
        {
            //Do any updates related to Attack UI stuff here.
            rootObject.SetActive(true);
            nameOutput.SetState(UIElementState.ATTACKED);
            targetNumberOutput.SetState(UIElementState.ATTACKED);
            currentHealthOutput.SetState(UIElementState.ATTACKED);
            maxHealthOutput.SetState(UIElementState.ATTACKED);
        }

        public void OnDeath()
        {
            StartCoroutine(DisableTimer());
        }

        private IEnumerator DisableTimer()
        {
            //Do any updates related to Death UI stuff here.
            nameOutput.SetState(UIElementState.DEAD);
            targetNumberOutput.SetState(UIElementState.DEAD);
            currentHealthOutput.SetState(UIElementState.DEAD);
            maxHealthOutput.SetState(UIElementState.DEAD);
            yield return new WaitForSeconds(0.5F);
            nameOutput.SetState(UIElementState.DISABLED);
            targetNumberOutput.SetState(UIElementState.DISABLED);
            currentHealthOutput.SetState(UIElementState.DISABLED);
            maxHealthOutput.SetState(UIElementState.DISABLED);
        }
    }
}