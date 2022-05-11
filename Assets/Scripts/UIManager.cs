using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Utils;

public class UIManager : Singleton {
    private DialogueText dialogueText;
    private Image menu;
    public static UIManager INSTANCE { get; private set; }
    private Dialogue currentDialogue = null;

    protected override void Initialize() {
        dialogueText = FindObjectsOfType<DialogueText>(true).First(it => it.gameObject.CompareTag("DialogueText"));
        menu = FindObjectsOfType<Image>(true).First(it => it.gameObject.CompareTag("Menu"));
    }

    protected override Singleton instance {
        get => INSTANCE;
        set => INSTANCE = (UIManager) value;
    }

    public void Interact() {
        switch (GameManager.INSTANCE.currentGameState) {
            case GameState.WORLD:
                var interactible = GameManager.INSTANCE.FindObjectInFrontOfPlayer(LayerMask.GetMask("Interactible"));
                if (interactible != null) {
                    InteractWith(interactible);
                }

                break;
            case GameState.UI:
                AdvanceDialogue();
                break;
        }
    }

    public void InteractWith(GameObject gameObject) {
        currentDialogue = gameObject.GetComponent<Dialogue>();
        Debug.Log("HELLO");
        if (currentDialogue != null) {
            currentDialogue.startDialogue();
            GameManager.INSTANCE.TransitionGameState(GameState.UI);
            dialogueText.gameObject.parent().SetActive(true);

            dialogueText.textValue = currentDialogue.current()?.text;
        }
    }

    public void Cancel() {
        switch (GameManager.INSTANCE.currentGameState) {
            case GameState.UI:
                if (currentDialogue == null && menu.gameObject.activeInHierarchy) {
                    menu.gameObject.SetActive(false);
                    GameManager.INSTANCE.TransitionGameState(GameState.WORLD);
                } else if (currentDialogue != null) {
                    AdvanceDialogue();
                }

                break;
            case GameState.WORLD:
                menu.gameObject.SetActive(true);
                GameManager.INSTANCE.TransitionGameState(GameState.UI);
                break;
            case GameState.COMBAT:
                break;
        }
    }

    private void AdvanceDialogue() {
        if (!dialogueText.revealed) {
            dialogueText.RevealAll();
            return;
        }

        currentDialogue.advance();
        var current = currentDialogue.current();
        if (current == null) {
            currentDialogue = null;
            GameManager.INSTANCE.TransitionGameState(GameState.WORLD);
            dialogueText.gameObject.parent().SetActive(false);
        } else {
            dialogueText.textValue = current.Value.text;
        }
    }
    
}
