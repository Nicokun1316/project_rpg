using System;
using UnityEngine;

namespace DefaultNamespace {
    public class SimpleDialogue : MonoBehaviour, Dialogue {
        [SerializeField] private String text;
        private DialogueChunk? currentChunk = null;
        public void startDialogue() {
            currentChunk = new DialogueChunk(null, text);
        }

        public DialogueChunk? current() {
            return currentChunk;
        }

        public void advance(string option = null) {
            currentChunk = null;
        }
    }
}
