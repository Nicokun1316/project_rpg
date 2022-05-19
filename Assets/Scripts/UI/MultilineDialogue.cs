using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class MultilineDialogue : MonoBehaviour, Dialogue {
        [SerializeField] private List<String> dialogues;
        private int currentIndex = 0;
        private event Dialogue.OnDialogueFinished onFinished;
        public void startDialogue() {
            currentIndex = 0;
        }

        public DialogueChunk? current() {
            if (currentIndex < dialogues.Count) {
                return new DialogueChunk("", dialogues[currentIndex]);
            } else {
                onFinished?.Invoke();
                return null;
            }
        }

        public void advance(string option = null) {
            ++currentIndex;
        }

        public void AddFinishedListener(Dialogue.OnDialogueFinished listener) {
            onFinished += listener;
        }
    }
}
