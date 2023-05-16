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
    public delegate bool InputHook();

    public void RegisterInputHook(String action, InputHook hook) {
        hooks.Add(action, hook);
    }

    public void UnregisterInputHook(String action) {
        hooks.Remove(action);
    }

    private bool ProcessHooks(String name) {
        if (hooks.ContainsKey(name)) {
            var delete = hooks[name]();
            if (delete) {
                UnregisterInputHook(name);
            }

            return true;
        }

        return false;
    }

    public void Move(Vector2 vec) {
        switch (GameManager.INSTANCE.CurrentGameState) {
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
        SendMovementInput(context.ReadValue<Vector2>(), context.action.name);
        //Move(resultVector);
    }

    public void SendMovementInput(Vector2 v, String action) {
        if (ProcessHooks(action)) return;
        
        var mv = v;
        Vector2 resultVector;
        if (mv == Vector2.zero) {
            resultVector = mv;
        } else if (Math.Abs(mv.x) > Math.Abs(mv.y)) {
            resultVector = new Vector2(mv.x > 0 ? 1 : -1, 0);
        } else {
            resultVector = new Vector2(0, mv.y > 0 ? 1 : -1);
        }
        
        uiMoveMagician.SetMovementVector(resultVector);
        
    }

    private void Confirm(InputAction.CallbackContext context) {
        if (!context.performed || ProcessHooks(context.action.name)) {
            return;
        }
        var currentState = GameManager.INSTANCE.CurrentGameState;
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

        if (ProcessHooks(context.action.name)) {
            return;
        }

        switch (GameManager.INSTANCE.CurrentGameState) {
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
        if (GameManager.INSTANCE.CurrentGameState is GameState.WORLD or GameState.UI) {
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
