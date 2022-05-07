using System;

namespace DefaultNamespace {
    public interface Dialogue {
        public void startDialogue();
        public DialogueChunk? current();
        public void advance(String option = null);
    }
}
