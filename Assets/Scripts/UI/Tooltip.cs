using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace UI {
    [RequireComponent(typeof(BoxCollider2D))]
    public class Tooltip : MonoBehaviour {
        private TMP_Text text;
        private CancellationTokenSource source;
        
        private float duration = 0.3f;
        private float elapsedTime = 0.5f;

        private void Awake() {
            text = GetComponentInChildren<TMP_Text>(true);
            GetComponent<BoxCollider2D>().isTrigger = true;
            text.alpha = 0;
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("Player")) {
                source?.Cancel();
                source = new CancellationTokenSource();
                FadeIn(source.Token).Forget();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                source?.Cancel();
                source = new CancellationTokenSource();
                FadeOut(source.Token).Forget();
            }
        }

        private async UniTask FadeIn(CancellationToken token) {
            var dur = elapsedTime;
            var initAlpha = text.alpha;
            elapsedTime = 0;
            while (elapsedTime < dur) {
                elapsedTime = Math.Min(elapsedTime + Time.deltaTime, dur);
                text.alpha = Mathf.Lerp(initAlpha, 1, elapsedTime / dur);
                await UniTask.NextFrame(token);
            }

            elapsedTime = duration;
        }

        private async UniTask FadeOut(CancellationToken token) {
            var dur = elapsedTime;
            var initAlpha = text.alpha;
            elapsedTime = 0;
            while (elapsedTime < dur) {
                elapsedTime = Math.Min(elapsedTime + Time.deltaTime, dur);
                text.alpha = Mathf.Lerp(initAlpha, 0, elapsedTime / dur);
                await UniTask.NextFrame(token);
            }

            elapsedTime = duration;
        }
    }
}
