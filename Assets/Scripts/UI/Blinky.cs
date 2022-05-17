using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class Blinky : MonoBehaviour {
        private Image image;
        //private IEnumerator runningBlink;
        private Color initialColor;
        private Animator animator;
        private static readonly Color Transparent = new(1, 1, 1, 0);
        private void Awake() {
            image = GetComponent<Image>();
            animator = GetComponent<Animator>();
            initialColor = image.color;
        }

        private void OnEnable() {
            StartAnimation();
        }

        private void OnDisable() {
            StopAnimation();
        }

        private IEnumerator DelaySwitchTilNextUpdate(bool enabled) {
            yield return new WaitForNextFrameUnit();
            animator.enabled = enabled;
        }

        /*private IEnumerator Blink() {
            while (true) {
                //image.color = initialColor;
                yield return new WaitForSeconds(0.5f);
                //image.color = Transparent;
                yield return new WaitForSeconds(0.5f);
            }
        }*/

        public void StopAnimation() {
            if (gameObject.activeInHierarchy) {
                animator.Play(null, -1, 0);
                StartCoroutine(DelaySwitchTilNextUpdate(false));
            }

            image.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0.5f);
        }

        public void StartAnimation() {
            if (gameObject.activeInHierarchy)
                StartCoroutine(DelaySwitchTilNextUpdate(true));
            image.color = initialColor;
        }
    }
}
