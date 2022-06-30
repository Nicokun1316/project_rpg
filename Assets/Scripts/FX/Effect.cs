using System;
using Cysharp.Threading.Tasks;
using Items;
using UnityEngine;

namespace FX {
    public static class Effect {
        public static async UniTask Map(float duration, Action<float> setter) {
            float passed = 0;
            while (passed <= duration) {
                await UniTask.NextFrame();
                passed += Time.deltaTime;
                float t = Mathf.Min(1, passed / duration);
                setter(t);
            }
        }
        public static async UniTask Lerp(float from, float to, float duration, Action<float> setter) {
            await Map(duration, (t) => setter(Mathf.Lerp(from, to, t)));
        }

        public static async UniTask FadeOut(float duration, Action<float> setter) {
            await Lerp(1, 0, duration, setter);
        }

        public static async UniTask FadeIn(float duration, Action<float> setter) {
            await Lerp(0, 1, duration, setter);
        }
    }
}
