using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace {
    public class RevealingText {
        private readonly String text;
        public Observable<String> currentText;
        public bool revealed { get; private set; }

        public RevealingText(String text) {
            this.text = text;
            revealed = false;
            currentText = "";
        }

        public IEnumerator RevealText() {
            revealed = false;
            Debug.Log("Beginning the begin");
            foreach (var character in text) {
                Debug.Log($"Adding character `{character}`");
                currentText.set(currentText.get() + character);
                yield return new WaitForSeconds(0.1f);
            }

            revealed = true;
        }

        public void RevealAll() {
            revealed = true;
            currentText.set(text);
        }
    }
}
