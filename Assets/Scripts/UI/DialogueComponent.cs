using System.Linq;
using UnityEngine;
using Utils;

namespace UI {
    public class DialogueComponent : MonoBehaviour, Focusable {
        private DialogueText textComponent;
        private Dialogue dialogue;

        private void Awake() {
            dialogue = GetComponent<Dialogue>();
            textComponent = GameObject.FindObjectsOfType<DialogueText>(true).First(it => it.CompareTag("DialogueText"));
        }

        public ConfirmResult MoveInput(Vector2 direction) {
            return direction == Vector2.down ? AdvanceDialogue() : ConfirmResult.DoNothing;
        }

        public ConfirmResult Confirm() {
            return AdvanceDialogue();
        }

        public ConfirmResult Cancel() {
            return AdvanceDialogue();
        }

        public ConfirmResult Focus() {
            dialogue.startDialogue();
            textComponent.gameObject.parent().SetActive(true);
            textComponent.textValue = dialogue.current()?.text;
            return ConfirmResult.DoNothing;
        }

        public void Unfocus() {
            textComponent.gameObject.parent().SetActive(false);
        }

        public void Freeze() {
            // ignore
        }

        public void Unfreeze() {
            // ignore
        }

        private ConfirmResult AdvanceDialogue() {
            if (!textComponent.revealed) {
                textComponent.Continue();
                return ConfirmResult.DoNothing;
            }
            dialogue.advance();
            var currentChunk = dialogue.current();
            if (currentChunk == null) {
                return ConfirmResult.Return;
            } else {
                textComponent.textValue = currentChunk.Value.text;
                return ConfirmResult.DoNothing;
            }
        }
    }
}
