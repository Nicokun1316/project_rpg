using System;
using UI;
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
        Vector2 resultVector;
        if (mv == Vector2.zero) {
            resultVector = mv;
        } else if (Math.Abs(mv.x) > Math.Abs(mv.y)) {
            resultVector = new Vector2(mv.x > 0 ? 1 : -1, 0);
        } else {
            resultVector = new Vector2(0, mv.y > 0 ? 1 : -1);
        }
        
        switch (GameManager.INSTANCE.currentGameState) {
            case GameState.UI:
                if (context.performed) {
                    UIManager.INSTANCE.MoveUI(resultVector);
                }

                break;
            case GameState.WORLD:
                playerController.Move(resultVector);
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
