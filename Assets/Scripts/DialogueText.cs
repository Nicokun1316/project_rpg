using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

namespace DefaultNamespace {
    public class DialogueText: TextMeshProUGUI {
        private RevealingText rText = null;
        private IEnumerator runningText = null;

        private readonly Observable<String>.ValueChange updateText;
        
        public bool revealed => rText.revealed;

        public DialogueText() {
            updateText = (_, newValue) => {
                text = newValue;
            };
        }

        public String textValue {
            get => rText.completeText;
            set {
                text = "";
                if (runningText is not null) {
                    rText.currentText.onChange -= updateText;
                    StopCoroutine(runningText);
                    runningText = null;
                }

                rText = new RevealingText(value);
                rText.currentText.onChange += updateText;
                runningText = rText.RevealText();
                StartCoroutine(runningText);
            }
        }

        public void RevealAll() {
            if (runningText != null) {
                StopCoroutine(runningText);
                rText.RevealAll();
                runningText = null;
                rText.currentText.onChange -= updateText;
            }
        }


        protected override void OnDisable() {
            base.OnDisable();
            if (runningText != null) {
                rText.currentText.onChange -= updateText;
                StopCoroutine(runningText);
                runningText = null;
            }
        }

        protected override void OnEnable() {
            base.OnEnable();
            if (rText != null) {
                rText.currentText.onChange += updateText;
                runningText = rText.RevealText();
                StartCoroutine(runningText);
            }
        }
    }
}
