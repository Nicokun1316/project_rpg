using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Dialogue {
    [Serializable]
    public class MultilineDialogue : /*MonoBehaviour,*/ Dialogue {
        [SerializeField] private List<DialogueChunk> dialogues;
        private int currentIndex = 0;
        private event Action onFinished;
        private event Action onStarted;

        public MultilineDialogue() {
            dialogues = new List<DialogueChunk>();
        }

        public MultilineDialogue(List<DialogueChunk> chunks) {
            dialogues = chunks;
        }

        public void initialize(List<DialogueChunk> dialogues) {
            this.dialogues = dialogues;
        }
        public void startDialogue() {
            currentIndex = 0;
            onStarted?.Invoke();
        }

        public DialogueChunk? current() {
            if (currentIndex < dialogues.Count) {
                return dialogues[currentIndex];
            } else {
                onFinished?.Invoke();
                return null;
            }
        }

        public void advance(string option = null) {
            ++currentIndex;
        }

        public void AddFinishedListener(Action listener) {
            onFinished += listener;
        }

        public void AddStartedListener(Action action) {
            onStarted += action;
        }
    }
}
