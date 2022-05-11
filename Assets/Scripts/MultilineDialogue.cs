using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace {
    public class MultilineDialogue : MonoBehaviour, Dialogue {
        [SerializeField] private List<String> dialogues;
        private int currentIndex = 0;
        public void startDialogue() {
            currentIndex = 0;
        }

        public DialogueChunk? current() {
            if (currentIndex < dialogues.Count) {
                return new DialogueChunk("", dialogues[currentIndex]);
            } else return null;
        }

        public void advance(string option = null) {
            ++currentIndex;
        }
    }
}
