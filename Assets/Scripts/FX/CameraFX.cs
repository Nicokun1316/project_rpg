using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraFX : MonoBehaviour {
    private CameraMan cameraMan;

    private void Awake() {
        cameraMan = GetComponent<CameraMan>();
    }

    public async UniTask Shake(float magnitude, float duration) {
        cameraMan.enabled = false;
        var originalPosition = transform.localPosition;
        while (duration > 0) {
            transform.localPosition = originalPosition + Random.insideUnitSphere * magnitude;
            duration -= Time.fixedDeltaTime;
            await UniTask.WaitForFixedUpdate();
        }

        transform.localPosition = originalPosition;
        cameraMan.enabled = true;
    }
}