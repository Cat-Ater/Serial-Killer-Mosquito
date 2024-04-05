using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class handling passing lines of text to a UI TMP output. 
/// </summary>
namespace UI.System
{
    public partial class UI_DialogueDisplay : MonoBehaviour
    {
        IDialogueCaller _caller;
        int _lineIndex;
        int _charIndex;
        bool skipEnabled = false;
        public Animator animator; 
        [Header("Textbox used to display character name.")]
        public TextMeshProUGUI nameOutput; 
        [Header("Textbox used to display line output.")]
        public TextMeshProUGUI textOutput;
        public UI_DialogueState _dialogueState = UI_DialogueState.INACTIVE;
        public UIDialogueData dialogueData; 

        public string DisplayStr
        {
            set => textOutput.text = value;
            get => textOutput.text;
        }

        private void OnEnable() => ResetState(false);

        public void SetInstanceUp(IDialogueCaller caller, UIDialogueData data)
        {
            //Set data. 
            _dialogueState = UI_DialogueState.INIT;
            _caller = caller;
            //Set the data. 
            dialogueData = data; 

            //Reset values. 
            ResetState(true);

            //TODO: IMPLEMENT THIS.
            ///PlayerController.Instance.PlayerEnabled = false;
            UIManager.PlaySound = data.textOpenSFX;
            animator.SetBool("IsOpen", true);

            //Begin print.
            StartCoroutine(PrintLine(dialogueData.lines[_lineIndex]));
        }

        private void ResetState(bool displayState)
        {
            _lineIndex = 0;
            textOutput.text = "";
            textOutput.enabled = displayState;
        }

        void Update()
        {
            if (_dialogueState == UI_DialogueState.INACTIVE)
                return;

            if (Input.anyKey && _dialogueState == UI_DialogueState.SCROLLING && skipEnabled)
            {
                UIManager.PlaySound = dialogueData.textSkipSFX;
                _dialogueState = UI_DialogueState.SCROLL_INTERUPT;
                StartCoroutine(KeypressSpamPrevention());
            }
            if (Input.anyKey && _dialogueState == UI_DialogueState.LINE_WAIT)
            {
                _lineIndex++;

                if (_lineIndex < dialogueData.LineCount)
                    StartCoroutine(PrintLine(dialogueData.lines[_lineIndex]));
                else
                {
                    UIManager.PlaySound = dialogueData.textCloseSFX;
                    _dialogueState = UI_DialogueState.INACTIVE;

                    //TODO: Implement this. 
                    ///PlayerController.Instance.PlayerEnabled = true;
                    animator.SetBool("IsOpen", false);
                    _caller.DisplayComplete();
                    ResetState(false);
                }
            }
        }

        private IEnumerator KeypressSpamPrevention()
        {
            yield return new WaitForSeconds(0.5f);
            skipEnabled = true;
        }

        private IEnumerator PrintLine(UIDialogueLine line)
        {
            //Set the state. 
            _dialogueState = UI_DialogueState.SCROLLING;
            //Calculate the line scroll speed. 
            float perCharSpeed = line.CharSpeed;
            DisplayStr = string.Empty;
            _charIndex = 0;

            //Set the characters name: 
            textOutput.name = line.name; 

            while (DisplayStr.Length < line.TextLength && _dialogueState != UI_DialogueState.SCROLL_INTERUPT)
            {
                DisplayStr += line.text[_charIndex];
                UIManager.PlaySound = dialogueData.textTypeSFX;
                yield return new WaitForSeconds(perCharSpeed);
                _charIndex++;
            }

            //Update the state. 
            if (_dialogueState == UI_DialogueState.SCROLL_INTERUPT)
                DisplayStr = line.text;

            _dialogueState = UI_DialogueState.LINE_WAIT;
        }
    }
}

