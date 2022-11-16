using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class InputController : MovementController {
    private Vector2 moveVec;

    private void Awake() {
        KeepMoving(this.GetCancellationTokenOnDestroy()).Forget();
    }

    private void Update() {
        if (!GameManager.IsPhysicsEnabled() || GameManager.INSTANCE.CurrentGameState != GameState.WORLD) {
            SetMovementVector(Vector2.zero);
        }
    }

    public void SetMovementVector(Vector2 moveVec) {
        this.moveVec = moveVec;
    }

    private async UniTask KeepMoving(CancellationToken token = default) {
        while (!token.IsCancellationRequested) {
            await UniTask.WaitUntil(() => moveVec != Vector2.zero && GameManager.IsPhysicsEnabled(), cancellationToken: token);
            await MoveCharacter(moveVec);
        }
    }
}