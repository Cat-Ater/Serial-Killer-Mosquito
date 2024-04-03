using UnityEngine;
using System;

namespace UI
{
    /// <summary>
    /// Class containing data related to dialogue lines. 
    /// </summary>
    [Serializable]
    public struct UIDialogueLine
    {
        [Header("The name to display.")]
        public string name;
        [Header("The string of text to display.")]
        [TextArea(3, 10)]
        public string text;
        [Header("The speed to complete text display in.")]
        public float speedOfLine;

        /// <summary>
        /// The speed at which the line should be displayed on a per character rate. 
        /// </summary>
        public float CharSpeed => speedOfLine / text.Length;

        /// <summary>
        /// The length of the line of text. 
        /// </summary>
        public int TextLength => text.Length;
    }
}

