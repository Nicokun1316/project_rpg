using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using Utils;

public class UIManager : MonoBehaviour {
    [SerializeField] private TMP_Text dialogueText;
    public static UIManager INSTANCE { get; private set; }
    private Dialogue currentDialogue = null;
    private RevealingText currentDialogueText = null;

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
                    dialogueText.text = current.Value.text;
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
            currentDialogueText = new RevealingText(currentDialogue.current()?.text);
            currentDialogueText.currentText.onChange += (oldValue, newValue) => {
                Debug.Log(newValue);
                dialogueText.text = newValue;
            };
            StartCoroutine(currentDialogueText.RevealText());
            // dialogueText.text = currentDialogue.current()?.text;
        }
    }
}
