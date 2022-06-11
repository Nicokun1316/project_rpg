using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Cutscene.AbyssIntro {
    public class FrightfulDisappearance : MonoBehaviour {
        private new Camera camera;
        private bool disappearing = false;

        private void Awake() {
            camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        public void Show() {
            gameObject.SetActive(true);
            disappearing = false;
        }
        
        void Update() {
            var campos = camera.WorldToViewportPoint(transform.position);
            if (!disappearing && campos.x is > 0 and < 1 && campos.y is > 0 and < 1 && gameObject.activeSelf) {
                Disappear().Forget();
            }
        }

        private async UniTask Disappear() {
            disappearing = true;
            await UniTask.Delay(TimeSpan.FromMilliseconds(500));
            gameObject.SetActive(false);
        }
    }
}
