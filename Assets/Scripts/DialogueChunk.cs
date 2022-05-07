using System;
using System.Collections.Generic;

namespace DefaultNamespace {
    public struct DialogueChunk {
        public DialogueChunk(String author, String text, List<String> options = null) {
            this.author = author;
            this.text = text;
            this.options = options;
        }
        public readonly String author;
        public readonly List<String> options;
        public readonly String text;
    }
}
