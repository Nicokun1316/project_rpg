using System;
using UnityEngine;

namespace UI.Dialogue {
    [Serializable]
    public class SimpleDialogue : /*MonoBehaviour,*/ Dialogue {
        [SerializeField] private DialogueChunk text;
        private DialogueChunk? currentChunk = null;
        private event Dialogue.OnDialogueFinished onFinished;
        public SimpleDialogue() {}

        public SimpleDialogue(DialogueChunk chunk) {
            text = chunk;
        }

        public void startDialogue() {
            currentChunk = text;
        }

        public DialogueChunk? current() {
            if (currentChunk == null) {
                onFinished?.Invoke();
            }
            return currentChunk;
        }

        public void advance(string option = null) {
            currentChunk = null;
        }

        public void AddFinishedListener(Dialogue.OnDialogueFinished listener) {
            onFinished += listener;
        }
    }
}
