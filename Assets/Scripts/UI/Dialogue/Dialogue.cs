using System;

namespace UI.Dialogue {
    public interface Dialogue {
        public void startDialogue();
        public DialogueChunk? current();
        public void advance(String option = null);
        public void AddFinishedListener(Action listener);
        public void AddStartedListener(Action action);
    }
}
