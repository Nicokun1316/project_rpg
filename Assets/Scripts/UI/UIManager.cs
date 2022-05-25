using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

namespace UI {
    public class UIManager : Singleton {
        //private Image menu;
        public static UIManager INSTANCE { get; private set; }
        private StaticChoice choice;
        private Stack<Focusable> focusStack = new();
        private Object lastObservedState;
        [SerializeField] public UIMenuItem itemPrefab;

        protected override void Initialize() {
            choice = FindObjectsOfType<StaticChoice>(true).First(it => it.gameObject.CompareTag("Menu"));
        }

        protected override Singleton instance {
            get => INSTANCE;
            set => INSTANCE = (UIManager) value;
        }

        public void MoveUI(Vector2 direction) {
            var result = focusStack.Peek().MoveInput(direction);
            PerformActionResult(result);
        }

        public void PerformInteraction(Focusable focusable) {
            GameManager.INSTANCE.TransitionGameState(GameState.UI);
            focusStack.Push(focusable);
            focusable.Focus();
        }

        public IEnumerator PerformDialogue(Dialogue dialogue, Action<Dictionary<String, String>> f = null) {
            var dc = DialogueComponent.Create(dialogue);
            yield return PerformInteractionAsync(dc, v => f?.Invoke(v as Dictionary<String, String>));
            Destroy(dc.gameObject);
            var dm = dialogue as MonoBehaviour;
            if (dm != null) {
                Destroy(dm.gameObject);
            }
        }

        public IEnumerator PerformInteractionAsync(Focusable focusable, Action<Object> f = null) {
            PerformInteraction(focusable);
            yield return new WaitUntil(() => GameManager.INSTANCE.currentGameState == GameState.WORLD);
            f?.Invoke(lastObservedState);
        }

        public void Interact() {
            switch (GameManager.INSTANCE.currentGameState) {
                case GameState.WORLD: {
                    var interactible = GameManager.INSTANCE.FindObjectInFrontOfPlayer(LayerMask.GetMask("Interactible"));
                    if (interactible != null) {
                        var focusable = interactible.GetComponent<Focusable>();
                        if (focusable != null) {
                            PerformInteraction(focusable);
                        }
                    }

                    break;
                }
                case GameState.UI: {
                    var focusable = focusStack.Peek();
                    var result = focusable.Confirm();
                    lastObservedState = focusable.State();
                    PerformActionResult(result);
                    break;
                }
            }
        }

        public void Cancel() {
            switch (GameManager.INSTANCE.currentGameState) {
                case GameState.UI: {
                    var focusable = focusStack.Peek();
                    var result = focusable.Cancel();
                    PerformActionResult(result);
                    lastObservedState = focusable.State();
                    break;
                }
                case GameState.WORLD: {
                    GameManager.INSTANCE.TransitionGameState(GameState.UI);
                    var focusable = choice.GetComponent<Focusable>();
                    focusable.Focus();
                    focusStack.Push(focusable);
                    break;
                }
                case GameState.COMBAT:
                    break;
            }
        }

        private void PerformActionResult(ConfirmResult result) {
            switch (result.type) {
                case ConfirmResultType.DoNothing:
                    break;
                case ConfirmResultType.Return: {
                    var focusable = focusStack.Pop();
                    focusable.Unfocus();
                    
                    while (focusStack.Count > 0 && focusStack.Peek().ShouldPop()) {
                        focusStack.Pop();
                    }
                    
                    if (focusStack.Count > 0) {
                        focusStack.Peek().Unfreeze();
                    } else {
                        GameManager.INSTANCE.TransitionGameState(GameState.WORLD);
                    }

                    break;
                }
                case ConfirmResultType.ChangeFocus: { // why is this a thing in any language made after C
                    var focusable = focusStack.Peek();
                    focusable.Freeze();
                    focusStack.Push(result.newFocusTarget);
                    var r = result.newFocusTarget.Focus();
                    PerformActionResult(r);
                    break;
                }
            }
        }
    }
}
