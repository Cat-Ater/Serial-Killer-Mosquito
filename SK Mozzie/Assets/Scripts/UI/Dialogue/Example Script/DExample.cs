using System.Collections;
using UnityEngine;
using UI;

namespace UI
{
    public abstract class NarrativeCaller : MonoBehaviour, IDialogueCaller
    {
        public UIDialogueData data;

        public void DisplayScript() => UIManager.SetDialogue(this, data);

        public void DisplayComplete() => DialogueEnded();

        public abstract void DialogueEnded();
    }

    public abstract class NarrativeEvent : NarrativeCaller
    {

    }

    public class DExample : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}