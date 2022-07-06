using System;
using System.Collections;
using UnityEngine;

namespace UI {
    public class RevealingText {
        public readonly String completeText;
        public Observable<String> currentText;
        public bool revealed { get; private set; }
        private bool continueText = false;
        private bool isParsingTags = false;

        public RevealingText(String completeText) {
            this.completeText = completeText;
            revealed = false;
            currentText = "";
        }

        public IEnumerator RevealText() {
            revealed = false;
            UIAudioManager.INSTANCE.Play();
            foreach (var character in completeText) {
                switch (character) {
                    case '|':
                        continueText = false;
                        UIAudioManager.INSTANCE.Stop();
                        yield return new WaitUntil(() => continueText);
                        UIAudioManager.INSTANCE.Play();
                        continueText = false;
                        break;
                    case '<':
                        currentText.Set(currentText.Get() + character);
                        isParsingTags = true;
                        break;
                    default: {
                        currentText.Set(currentText.Get() + character);
                        if (isParsingTags) {
                            if (character == '>') isParsingTags = false;
                        } else {
                            if (!continueText) {
                                yield return new WaitForSecondsRealtime(0.05f);
                            }
                        }

                        break;
                    }
                }
            }
            UIAudioManager.INSTANCE.Stop();

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
            currentText.Set(completeText);
        }
    }
}
