using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    private GameObject player;

    private Rigidbody2D playerBody;

    private MovementController playerController;

    private ContactFilter2D interactibleFilter;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player");
        playerBody = player.GetComponent<Rigidbody2D>();
        playerController = player.GetComponent<MovementController>();
        interactibleFilter.SetLayerMask(LayerMask.GetMask("Interactible"));
        interactibleFilter.useLayerMask = true;
    }

    public void Move(InputAction.CallbackContext context) {
        var mv = context.ReadValue<Vector2>();
        switch (GameManager.INSTANCE.currentGameState) {
            case GameState.UI:
                break;
            case GameState.WORLD:
                playerController.Move(mv);
                break;
            case GameState.COMBAT:
                break;
            
        }
    }

    public void Confirm(InputAction.CallbackContext context) {
        if (!context.performed) {
            return;
        }
        var currentState = GameManager.INSTANCE.currentGameState;
        switch (currentState) {
            case GameState.UI:
                UIManager.INSTANCE.Interact();
                break;
            case GameState.WORLD:
                UIManager.INSTANCE.Interact();
                break;
            case GameState.COMBAT:
                break;
        }
    }

    public void Cancel(InputAction.CallbackContext context) {
        if (!context.performed) {
            return;
        }

        switch (GameManager.INSTANCE.currentGameState) {
            case GameState.UI:
                UIManager.INSTANCE.Cancel();
                break;
            case GameState.WORLD:
                UIManager.INSTANCE.Cancel();
                break;
            case GameState.COMBAT: 
                break;
        }
    }
}
