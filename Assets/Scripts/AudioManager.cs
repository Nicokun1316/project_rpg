using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AudioManager : Singleton {
    private AudioSource source;
    protected override Singleton instance { get => INSTANCE; set => INSTANCE = (AudioManager) value; }
    protected override void Initialize() {
        source = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip) {
        ChangeSongs(clip).Forget();
    }

    public AudioClip CurrentClip => source.clip;

    public static AudioManager INSTANCE { get; private set; }

    private async UniTask ChangeSongs(AudioClip clip) {
        const float initTime = 1.5f;
        var t = initTime;
        while (t >= 0) {
            await UniTask.NextFrame(PlayerLoopTiming.Update);
            source.volume = Mathf.SmoothStep(1, 0, t / initTime);
            t -= Time.unscaledDeltaTime;
        }

        source.volume = 0;

        source.clip = clip;
        source.Play();
        await UniTask.Delay(TimeSpan.FromMilliseconds(300), true);

        t = initTime;
        while (t >= 0) {
            await UniTask.NextFrame(PlayerLoopTiming.Update);
            source.volume = Mathf.SmoothStep(0, 1, t / initTime);
            t -= Time.unscaledDeltaTime;
        }

        source.volume = 1;
    }
}
