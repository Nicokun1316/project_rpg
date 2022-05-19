using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class StatefulDialogue : MonoBehaviour, Dialogue {
        [SerializeField] private List<TextSequence> dialogues;
        private int dialogueIndex = -1;
        private int textIndex = 0;
        private event Dialogue.OnDialogueFinished onFinished;
        public void startDialogue() {
            if (dialogueIndex + 1 < dialogues.Count)
                ++dialogueIndex;
            textIndex = 0;
        }

        public DialogueChunk? current() {
            var currentText = dialogues[dialogueIndex];
            if (textIndex < currentText.lines.Count) {
                return new DialogueChunk("", currentText.lines[textIndex]);
            } else {
                onFinished?.Invoke();
                return null;
            }
        }

        public void advance(string option = null) {
            ++textIndex;
        }

        public void AddFinishedListener(Dialogue.OnDialogueFinished listener) {
            onFinished += listener;
        }
    }
}
