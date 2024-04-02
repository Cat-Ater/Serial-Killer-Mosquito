using UnityEngine;
using System;

namespace UI
{
    /// <summary>
    /// Class containing data related to a dialogue session. 
    /// </summary>
    [Serializable]
    public struct UIDialogueData
    {
        [Header("The lines of text to display. ")]
        public UIDialogueLine[] lines;
        [Header("The AudioClip to play on opening the dialogue box. ")]
        public AudioClip textOpenSFX;
        [Header("The AudioClip to play on closing the dialogue box. ")]
        public AudioClip textCloseSFX;
        [Header("The AudioClip to play while writing out each character. ")]
        public AudioClip textTypeSFX;
        [Header("The AudioClip to play on skipping to the end of the dialogue line. ")]
        public AudioClip textSkipSFX;

        /// <summary>
        /// The number of lines within the dialogue. 
        /// </summary>
        public int LineCount => lines.Length;
    }
}

