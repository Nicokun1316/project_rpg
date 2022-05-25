using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Utils;

namespace UI {
    public class DialogueComponent : MonoBehaviour, Focusable {
        private DialogueText textComponent;
        private Dialogue dialogue;
        private GameObject answerPanel;
        private MenuChoice choices;
        private bool isChoosing;
        private String currentChoiceTag;
        private Dictionary<String, String> state;

        public static DialogueComponent Create(Dialogue dialogue) {
            var go = new GameObject("DialogueComponent");
            var dc = go.AddComponent<DialogueComponent>();
            dc.dialogue = dialogue;
            return dc;
        }

        private void Awake() {
            dialogue ??= GetComponent<Dialogue>();
            textComponent = GameObject.FindObjectsOfType<DialogueText>(true).First(it => it.CompareTag("DialogueText"));
            answerPanel = textComponent.transform.parent.Find("DialogueChoices").gameObject;
            choices = answerPanel.GetComponent<MenuChoice>();
        }

        public ConfirmResult MoveInput(Vector2 direction) {
            if (isChoosing) {
                if (direction == Vector2.left) {
                    choices.Previous();
                } else if (direction == Vector2.right) {
                    choices.Next();
                }
                return ConfirmResult.DoNothing;
            } else {
                return direction == Vector2.down ? AdvanceDialogue() : ConfirmResult.DoNothing;
            }
        }

        public ConfirmResult Confirm() {
            if (!isChoosing || !textComponent.revealed) {
                return AdvanceDialogue();
            } else {
                var choice = choices.currentSelection.text;
                state.Add(currentChoiceTag, choice);
                return AdvanceDialogue();
            }
        }

        public ConfirmResult Cancel() {
            return AdvanceDialogue();
        }

        public ConfirmResult Focus() {
            state = new();
            dialogue.startDialogue();
            textComponent.gameObject.parent().SetActive(true);
            textComponent.textValue = dialogue.current()?.Text;
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
            answerPanel.SetActive(false);
            foreach (Transform child in answerPanel.transform) {
                Destroy(child.gameObject);
            }
            isChoosing = false;
            if (currentChunk == null) {
                return ConfirmResult.Return;
            } else {
                var cc = currentChunk.Value;
                textComponent.textValue = cc.Text;
                if (cc.Options.Count > 0) {
                    isChoosing = true;
                    currentChoiceTag = cc.ChoiceTag;
                    foreach (var option in cc.Options) {
                        var item = Instantiate(UIManager.INSTANCE.itemPrefab, answerPanel.transform);
                        item.text = option;
                    }

                    choices.Reset();
                    answerPanel.SetActive(true);
                    choices.index = 0;
                }
                return ConfirmResult.DoNothing;
            }
        }

        public object State() {
            return state;
        }
    }
}
