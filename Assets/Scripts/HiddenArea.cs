using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenArea : MonoBehaviour {
    private new SpriteRenderer renderer;
    [SerializeField] private float duration;
    private IEnumerator currentAnimation = null;
    private float elapsedTime = 0;
    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        elapsedTime = duration;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            if (currentAnimation != null) {
                StopCoroutine(currentAnimation);
            }
            currentAnimation = FadeOut();
            StartCoroutine(currentAnimation);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (currentAnimation != null) {
                StopCoroutine(currentAnimation);
            }
            currentAnimation = FadeIn();
            StartCoroutine(currentAnimation);
        }
    }

    private IEnumerator FadeOut() {
        var dur = elapsedTime;
        var color = renderer.color;
        var initAlpha = color.a;
        elapsedTime = 0;
        while (elapsedTime < dur) {
            elapsedTime = Math.Min(elapsedTime + Time.deltaTime, dur);
            color.a = Mathf.SmoothStep(initAlpha, 0, elapsedTime / dur);
            renderer.color = color;
            yield return null;
        }

        elapsedTime = duration;
    }

    private IEnumerator FadeIn() {
        var dur = elapsedTime;
        var color = renderer.color;
        var initAlpha = color.a;
        elapsedTime = 0;
        while (elapsedTime < dur) {
            elapsedTime = Math.Min(elapsedTime + Time.deltaTime, dur);
            color.a = Mathf.Lerp(initAlpha, 1, elapsedTime / dur);
            renderer.color = color;
            yield return null;
        }

        elapsedTime = duration;
    }
}
