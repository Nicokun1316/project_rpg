using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BouncyLuminosity : MonoBehaviour {
    [SerializeField] private float amplitude;
    [SerializeField] private float minitude;
    [SerializeField] private float speed;
    [SerializeField] private float maxLumin;
    [SerializeField] private float minLumin;
    private new Light2D light;
    private bool isBlinking = false;
    private CancellationTokenSource source;

    public void StartBlinking() {
        source = new CancellationTokenSource();
        KeepBlinking(source.Token).Forget();
    }

    public async UniTask AwaitCancellation() {
        source?.Cancel();
        await UniTask.WaitWhile(() => isBlinking);
    }

    private void Start() {
        light = GetComponent<Light2D>();
        StartBlinking();
    }

    private async UniTask KeepBlinking(CancellationToken token) {
        isBlinking = true;
        while (!token.IsCancellationRequested) {
            await Blink(amplitude, minitude, maxLumin, minLumin, speed);
        }

        isBlinking = false;
    }

    public async UniTask Blink(float amplitude, float minitude, float maxLumin, float minLumin, float speed) {
        await LightenUp(amplitude, minitude, maxLumin, minLumin, speed / 2.0f);
        await Quiet(amplitude, minitude, maxLumin, minLumin, speed / 2.0f);
    }

    public async UniTask LightenUp(float amplitude, float minitude, float maxLumin, float minLumin, float speed) {
        await ChangeLighting(minitude, amplitude, minLumin, maxLumin, speed);
    }

    public async UniTask Quiet(float amplitude, float minitude, float maxLumin, float minLumin, float speed) {
        await ChangeLighting(amplitude, minitude, maxLumin, minLumin, speed);
    }

    private async UniTask ChangeLighting(float fromRadius, float toRadius, float fromLumin, float toLumin, float speed) {
        var duration = 0.0f;
        while (duration < speed) {
            var t = Math.Min(duration / speed, 1.0f);
            light.intensity = Mathf.Lerp(fromLumin, toLumin, t);
            light.pointLightOuterRadius = Mathf.Lerp(fromRadius, toRadius, t);
            duration += Time.deltaTime;
            await UniTask.NextFrame();
        }
    }
}
