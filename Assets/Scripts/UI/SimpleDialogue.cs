using System;
using UnityEngine;

namespace UI {
    public class SimpleDialogue : MonoBehaviour, Dialogue {
        [SerializeField] private String text;
        private DialogueChunk? currentChunk = null;
        private event Dialogue.OnDialogueFinished onFinished;
        public void startDialogue() {
            currentChunk = new DialogueChunk(null, text);
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
