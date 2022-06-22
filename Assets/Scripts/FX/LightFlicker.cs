using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

namespace FX {
    public class LightFlicker : MonoBehaviour {
        [SerializeField] private int throttleRate;
        [SerializeField] private float flickerSpeed;
        [SerializeField] private float minDistortion;
        [SerializeField] private float maxDistortion;
        private float baseIntensity;
        private Light2D lightSource;
        private CancellationTokenSource cancelSource;

        public void StartFlickering() {
            cancelSource = new CancellationTokenSource();
            Flicker(cancelSource.Token).Forget();
        }

        public void StopFlickering() {
            cancelSource.Cancel();
            cancelSource = null;
        }
        
        private void Start() {
            lightSource = GetComponent<Light2D>();
            baseIntensity = lightSource.intensity;
            StartFlickering();
        }

        private async UniTask Flicker(CancellationToken token = default) {
            while (!token.IsCancellationRequested) {
                var min = baseIntensity - minDistortion;
                var max = baseIntensity + maxDistortion;
                lightSource.intensity = Mathf.Lerp(lightSource.intensity, Random.Range(min, max),
                    flickerSpeed * Time.deltaTime);
                await UniTask.Delay(TimeSpan.FromMilliseconds(throttleRate), cancellationToken: token);
            }
        }
    }
}
