using System;
using Unity.VisualScripting;
using UnityEngine;

namespace UI {
    public class SimpleDialogue : MonoBehaviour, Dialogue {
        [SerializeField] private DialogueChunk text;
        private DialogueChunk? currentChunk = null;
        private event Dialogue.OnDialogueFinished onFinished;

        public static SimpleDialogue Create(DialogueChunk chunk) {
            var go = new GameObject("Dialogue");
            var sd = go.AddComponent<SimpleDialogue>();
            sd.text = chunk;
            return sd;
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
