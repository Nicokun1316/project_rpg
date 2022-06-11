using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace UI {
    public class UIMoveInputMagician : MonoBehaviour {
        [SerializeField] private int initialThrottle;
        [SerializeField] private int repeatThrottle;
        [SerializeField] private UnityEvent<Vector2> move;
        private Vector2 lastInput = Vector2.zero;
        private CancellationTokenSource cancellator = new();

        public void SetMovementVector(Vector2 vec) {
            if (vec == lastInput) {
                return;
            }

            lastInput = vec;
            cancellator.Cancel();
            cancellator = new CancellationTokenSource();
            PerformUIMovement(vec, cancellator.Token).Forget();
        }

        private async UniTask PerformUIMovement(Vector2 vec, CancellationToken token = default) {
            move.Invoke(vec);
            if (vec == Vector2.zero) return;
            await UniTask.Delay(initialThrottle, delayType: DelayType.Realtime, cancellationToken: token);
            print("Past initial throttle");
            if (token.IsCancellationRequested) return;
            print("Cancelled before initial throttle");
            while (!token.IsCancellationRequested) {
                move.Invoke(vec);
                await UniTask.Delay(repeatThrottle, delayType: DelayType.Realtime, cancellationToken: token);
                print("Awaited repeat delay");
            }
        }

    }
}
