using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Dialogue {
    public class StatefulDialogue : MonoBehaviour, Dialogue {
        [SerializeField] private List<TextSequence> dialogues;
        private int dialogueIndex = -1;
        private int textIndex = 0;
        private event Action onFinished;
        private event Action onStarted;
        public void startDialogue() {
            if (dialogueIndex + 1 < dialogues.Count)
                ++dialogueIndex;
            textIndex = 0;
            onStarted?.Invoke();
        }

        public DialogueChunk? current() {
            var currentText = dialogues[dialogueIndex];
            if (textIndex < currentText.lines.Count) {
                return currentText.lines[textIndex];
            } else {
                onFinished?.Invoke();
                return null;
            }
        }

        public void advance(string option = null) {
            ++textIndex;
        }

        public void AddFinishedListener(Action listener) {
            onFinished += listener;
        }

        public void AddStartedListener(Action action) {
            onStarted += action;
        }
    }
}
