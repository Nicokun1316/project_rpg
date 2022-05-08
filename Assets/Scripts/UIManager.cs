using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using Utils;

public class UIManager : MonoBehaviour {
    [SerializeField] private DialogueText dialogueText;
    public static UIManager INSTANCE { get; private set; }
    private Dialogue currentDialogue = null;

    private void Awake() {
        INSTANCE ??= this;
        DontDestroyOnLoad(INSTANCE);
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
                currentDialogue.advance();
                var current = currentDialogue.current();
                if (current == null) {
                    GameManager.INSTANCE.TransitionGameState(GameState.WORLD);
                    dialogueText.gameObject.parent().SetActive(false);
                } else {
                    dialogueText.textValue = current.Value.text;
                }
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
}
