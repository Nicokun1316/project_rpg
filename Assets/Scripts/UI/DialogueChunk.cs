using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    [Serializable]
    public struct DialogueChunk {
        public DialogueChunk(String author, String text, String choiceTag = null, List<String> options = null) {
            this.author = author;
            this.text = text;
            this.choiceTag = choiceTag;
            this.options = options;
        }

        [SerializeField] private String author;
        [SerializeField] [TextArea] private String text;
        [SerializeField] private String choiceTag;
        [SerializeField] private List<String> options;
        public String Author => author;
        public String Text => text;
        public String ChoiceTag => choiceTag;
        public List<String> Options => options;
    }
}
