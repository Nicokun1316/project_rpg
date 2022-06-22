using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton {
    private GameObject player;

    private InputController playerController;
    private Dictionary<String, InputHook> hooks;
    private UIMoveInputMagician uiMoveMagician;

    // this will be replaced eventually:tm: since its ugly and error prone
    public delegate bool InputHook(InputAction.CallbackContext context);

    public void RegisterInputHook(String action, InputHook hook) {
        hooks.Add(action, hook);
    }

    public void UnregisterInputHook(String action) {
        hooks.Remove(action);
    }

    private bool ProcessHooks(InputAction.CallbackContext context) {
        if (hooks.ContainsKey(context.action.name)) {
            var delete = hooks[context.action.name](context);
            if (delete) {
                UnregisterInputHook(context.action.name);
            }

            return true;
        }

        return false;
    }

    public void Move(Vector2 vec) {
        switch (GameManager.INSTANCE.currentGameState) {
            case GameState.UI:
                //if (context.performed) {
                UIManager.INSTANCE.MoveUI(vec);
                //}

                break;
            case GameState.WORLD:
                playerController.SetMovementVector(vec);
                break;
            case GameState.COMBAT:
                break;
            
        }
    }

    private void Move(InputAction.CallbackContext context) {
        if (ProcessHooks(context)) return;
        
        var mv = context.ReadValue<Vector2>();
        Vector2 resultVector;
        if (mv == Vector2.zero) {
            resultVector = mv;
        } else if (Math.Abs(mv.x) > Math.Abs(mv.y)) {
            resultVector = new Vector2(mv.x > 0 ? 1 : -1, 0);
        } else {
            resultVector = new Vector2(0, mv.y > 0 ? 1 : -1);
        }
        
        uiMoveMagician.SetMovementVector(resultVector);
        //Move(resultVector);
    }

    private void Confirm(InputAction.CallbackContext context) {
        if (!context.performed || ProcessHooks(context)) {
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

    private void Cancel(InputAction.CallbackContext context) {
        if (!context.performed) {
            return;
        }

        if (ProcessHooks(context)) {
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

    public void OpenMenu(InputAction.CallbackContext context) {
        if (GameManager.INSTANCE.currentGameState is GameState.WORLD or GameState.UI) {
            UIManager.INSTANCE.ToggleMenu();
        }
    }

    public static InputManager INSTANCE;

    protected override Singleton instance {
        get => INSTANCE;
        set => INSTANCE = value as InputManager;
    }

    protected override void Initialize() {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<InputController>();
        hooks = new();
        uiMoveMagician = GetComponent<UIMoveInputMagician>();
    }
}
