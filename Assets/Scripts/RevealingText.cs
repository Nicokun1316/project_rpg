using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace {
    public class RevealingText {
        public readonly String completeText;
        public Observable<String> currentText;
        public bool revealed { get; private set; }

        public RevealingText(String completeText) {
            this.completeText = completeText;
            revealed = false;
            currentText = "";
        }

        public IEnumerator RevealText() {
            revealed = false;
            foreach (var character in completeText) {
                currentText.set(currentText.get() + character);
                yield return new WaitForSeconds(0.05f);
            }

            revealed = true;
        }

        public void RevealAll() {
            revealed = true;
            currentText.set(completeText);
        }
    }
}
