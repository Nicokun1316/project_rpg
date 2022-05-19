using System;
using DefaultNamespace;

namespace UI {
    public interface Dialogue {
        public delegate void OnDialogueFinished();
        public void startDialogue();
        public DialogueChunk? current();
        public void advance(String option = null);
        public void AddFinishedListener(OnDialogueFinished listener);
    }
}
