using System;
using System.Collections;
using UnityEngine;

namespace UI {
    public class RevealingText {
        public readonly String completeText;
        public Observable<String> currentText;
        public bool revealed { get; private set; }
        private bool continueText = false;

        public RevealingText(String completeText) {
            this.completeText = completeText;
            revealed = false;
            currentText = "";
        }

        public IEnumerator RevealText() {
            revealed = false;
            foreach (var character in completeText) {
                if (character == '|') {
                    continueText = false;
                    yield return new WaitUntil(() => continueText);
                    continueText = false;
                } else {
                    currentText.set(currentText.get() + character);
                    if (!continueText) {
                        yield return new WaitForSeconds(0.05f);
                    }
                }
            }

            revealed = true;
        }

        public bool Continue() {
            if (revealed) {
                return true;
            } else {
                continueText = true;
                return false;
            }
        }

        public void RevealAll() {
            revealed = true;
            currentText.set(completeText);
        }
    }
}
