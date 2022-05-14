using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class Blinky : MonoBehaviour {
        private RawImage image;
        private IEnumerator runningBlink;
        private static readonly Color Opaque = new(1, 1, 1, 1);
        private static readonly Color Transparent = new(1, 1, 1, 0);
        private void Awake() {
            image = GetComponent<RawImage>();
        }

        private void OnEnable() {
            runningBlink = Blink();
            StartCoroutine(runningBlink);
        }

        private void OnDisable() {
            StopAnimation();
        }

        IEnumerator Blink() {
            while (true) {
                image.color = Opaque;
                yield return new WaitForSeconds(0.5f);
                image.color = Transparent;
                yield return new WaitForSeconds(0.5f);
            }
        }

        public void StopAnimation() {
            if (runningBlink != null) {
                StopCoroutine(runningBlink);
                image.color = Opaque;
            }
        }

        public void StartAnimation() {
            runningBlink = Blink();
            StartCoroutine(runningBlink);
        }
    }
}
