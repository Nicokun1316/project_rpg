using System;
using UnityEngine;

namespace UI.Dialogue {
    [Serializable]
    public class SimpleDialogue : /*MonoBehaviour,*/ Dialogue {
        [SerializeField] private DialogueChunk text;
        private DialogueChunk? currentChunk = null;
        private event Action onFinished;
        private event Action onStarted;
        public SimpleDialogue() {}

        public SimpleDialogue(DialogueChunk chunk) {
            text = chunk;
        }

        public void startDialogue() {
            currentChunk = text;
            onStarted?.Invoke();
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

        public void AddFinishedListener(Action listener) {
            onFinished += listener;
        }

        public void AddStartedListener(Action action) {
            onStarted += action;
        }
    }
}
