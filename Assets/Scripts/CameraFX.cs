using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraFX : MonoBehaviour {
    private CameraMan cameraMan;

    private void Awake() {
        cameraMan = GetComponent<CameraMan>();
    }

    public IEnumerator Shake(float magnitude, float duration) {
        cameraMan.enabled = false;
        var originalPosition = transform.localPosition;
        while (duration > 0) {
            transform.localPosition = originalPosition + Random.insideUnitSphere * magnitude;
            print(transform.localPosition);
            duration -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        transform.localPosition = originalPosition;
        cameraMan.enabled = true;
    }
}