using System;

namespace UI.Dialogue {
    public interface ForwardDialogue : Dialogue {
        public Dialogue dialogue { get; }
        void Dialogue.startDialogue() {
            dialogue.startDialogue();
        }

        DialogueChunk? Dialogue.current() => dialogue.current();

        void Dialogue.advance(string option) {
            dialogue.advance(option);
        }

        void Dialogue.AddFinishedListener(Action listener) {
            dialogue.AddFinishedListener(listener);
        }

        void Dialogue.AddStartedListener(Action action) {
            dialogue.AddStartedListener(action);
        }
    }
}
