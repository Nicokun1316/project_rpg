using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class InputController : MovementController {
    private Vector2 moveVec;

    private void Awake() {
        KeepMoving().Forget();
    }

    public void Mv(Vector2 moveVec) {
        this.moveVec = moveVec;
    }

    private async UniTask KeepMoving() {
        while (true) {
            await UniTask.WaitUntil(() => moveVec != Vector2.zero && GameManager.IsPhysicsEnabled());
            await MoveCharacter(moveVec);
        }
    }
}