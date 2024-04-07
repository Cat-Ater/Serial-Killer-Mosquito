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
        /// <summary>
        /// The lines contained within the dialouge. 
        /// </summary>
        [Header("The lines of text to display. ")]
        public UIDialogueLine[] lines;
        /// <summary>
        /// The audio clip to play on opening the text box. 
        /// </summary>
        [Header("The AudioClip to play on opening the dialogue box. ")]
        public AudioClip textOpenSFX;
        /// <summary>
        /// The audio clip to play on closing the text box. 
        /// </summary>
        [Header("The AudioClip to play on closing the dialogue box. ")]
        public AudioClip textCloseSFX;
        /// <summary>
        /// The audio clip to play on each character being typed. 
        /// </summary>
        [Header("The AudioClip to play while writing out each character. ")]
        public AudioClip textTypeSFX;
        /// <summary>
        /// The audio clip to play on skipping a dialogue line. 
        /// </summary>
        [Header("The AudioClip to play on skipping to the end of the dialogue line. ")]
        public AudioClip textSkipSFX;

        public bool pausePlayer;
        public bool skipEnabled; 
        /// <summary>
        /// The number of lines within the dialogue. 
        /// </summary>
        public int LineCount => lines.Length;
    }
}

