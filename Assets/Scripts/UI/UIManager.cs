using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UI.Dialogue;
using UnityEngine;
using Utils;
using Object = System.Object;

namespace UI {
    public class UIManager : Singleton {
        //private Image menu;
        public static UIManager INSTANCE { get; private set; }
        private StaticChoice mainMenu;
        private Stack<Focusable> focusStack = new();
        private Object lastObservedState;
        [SerializeField] public UIMenuItem itemPrefab;

        protected override void Initialize() {
            mainMenu = FindObjectsOfType<StaticChoice>(true).First(it => it.gameObject.CompareTag("Menu"));
        }

        protected override Singleton instance {
            get => INSTANCE;
            set => INSTANCE = (UIManager) value;
        }

        public void MoveUI(Vector2 direction) {
            var result = focusStack.Peek().MoveInput(direction);
            PerformActionResult(result);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void PerformInteraction(Focusable focusable) {
            GameManager.INSTANCE.TransitionGameState(GameState.UI);
            focusStack.Push(focusable);
            focusable.Focus();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public async UniTask<Dictionary<String, String>> PerformDialogueUnsafe(Dialogue.Dialogue dialogue) {
            var dc = DialogueComponent.Create(dialogue);
            var dict = await PerformInteractionAsync(dc) as Dictionary<String, String>;
            Destroy(dc.gameObject);
            var dm = dialogue as MonoBehaviour;
            if (dm != null) {
                Destroy(dm.gameObject);
            }

            return dict;
        }

        public async UniTask<String> Ask(String question, List<String> options, String author = null) {
            return (await PerformDialogue(new DialogueChunk(author, question, "question", options)))
                .GetOrDefault("question", options.Last());
        }

        public async UniTask<bool> Ask(String question, String author = null) {
            return await Ask(question, new List<string> {"Yes", "No"}, author) == "Yes";
        }
        
        public async UniTask<Dictionary<String, String>> PerformDialogue(Dialogue.Dialogue dialogue) {
            var result = await PerformDialogueUnsafe(dialogue);
            return result;
        }

        public async UniTask<Dictionary<String, String>> PerformDialogue(DialogueChunk chunk) {
            var dialogue = new SimpleDialogue(chunk);
            var result = await PerformDialogue(dialogue);
            return result;
        }

        public async UniTask<Dictionary<String, String>> PerformDialogue(List<DialogueChunk> chunks) {
            var dialogue = new MultilineDialogue(chunks);
            var result = await PerformDialogue(dialogue);
            return result;
        }

        public async UniTask<Object> PerformInteractionAsync(Focusable focusable) {
            PerformInteraction(focusable);
            await UniTask.WaitUntil(() => GameManager.INSTANCE.currentGameState == GameState.WORLD);
            return lastObservedState;
        }

        public void Interact() {
            switch (GameManager.INSTANCE.currentGameState) {
                case GameState.WORLD: {
                    if (!GameManager.IsPhysicsEnabled()) return;
                    var interactible = GameManager.INSTANCE.FindObjectInFrontOfPlayer(LayerMask.GetMask("Interactible"));
                    if (interactible != null) {
                        var focusable = interactible.GetComponent<Focusable>();
                        if (focusable != null) {
                            PerformInteraction(focusable);
                        } else {
                            interactible.GetComponent<InteractionTarget>()?.Interact();
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
                    ToggleMenu();
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

        public void ToggleMenu() {
            if (mainMenu.gameObject.activeInHierarchy) {
                while (focusStack.Count > 0) {
                    PerformActionResult(ConfirmResult.Return);
                }
            } else {
                GameManager.INSTANCE.TransitionGameState(GameState.UI);
                var focusable = mainMenu.GetComponent<Focusable>();
                focusable.Focus();
                focusStack.Push(focusable);
            }
        }
    }
}
