using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class InputController : MovementController {
    private Vector2 moveVec;

    private void Awake() {
        KeepMoving().Forget();
    }

    private void Update() {
        if (!GameManager.IsPhysicsEnabled() || GameManager.INSTANCE.currentGameState != GameState.WORLD) {
            SetMovementVector(Vector2.zero);
        }
    }

    public void SetMovementVector(Vector2 moveVec) {
        this.moveVec = moveVec;
    }

    private async UniTask KeepMoving() {
        while (true) {
            await UniTask.WaitUntil(() => moveVec != Vector2.zero && GameManager.IsPhysicsEnabled());
            await MoveCharacter(moveVec);
        }
    }
}