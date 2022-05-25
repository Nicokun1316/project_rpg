using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    [Serializable]
    public class AnswerableDialogueChunk {
        public AnswerableDialogueChunk(String text, String answerTag = null, List<String> answers = null) {
            this.text = text;
            this.answerTag = answerTag;
            this.answers = answers;
        }
        [SerializeField] private String text;
        [SerializeField] private String answerTag;
        [SerializeField] private List<String> answers;

        public String Text => text;
        public String AnswerTag => answerTag;
        public List<String> Answers => Answers;
    }
}
