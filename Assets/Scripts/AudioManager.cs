using System;
using System.Collections;
using System.Collections.Generic;
using Cutscene;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;

public class AudioManager : Singleton {
    private AudioSource source;
    private readonly LockableBoolean processing = new();
    protected override Singleton instance { get => INSTANCE; set => INSTANCE = (AudioManager) value; }
    protected override void Initialize() {
        source = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip) {
        ChangeSongs(clip).Forget();
    }

    public AudioClip CurrentClip => source.clip;

    public static AudioManager INSTANCE { get; private set; }

    public async UniTask StopPlaying(float quietDuration = 1.5f) {
        using var _ = await processing.AcquireLock();
        var leftOver = 0.0f;
        while (leftOver <= quietDuration) {
            await UniTask.NextFrame();
            source.volume = Mathf.SmoothStep(1, 0, Math.Max(0, leftOver / quietDuration));
            leftOver += Time.unscaledDeltaTime;
        }
    }

    private async UniTask StartPlaying(AudioClip clip, float t) {
        using var _ = await processing.AcquireLock();
        source.volume = 0;
        source.clip = clip;
        source.Play();
        var leftOver = 0.0f;
        while (leftOver <= t) {
            await UniTask.NextFrame();
            source.volume = Mathf.SmoothStep(0, 1, Math.Min(1, leftOver / t));
            leftOver += Time.unscaledDeltaTime;
        }
        
    }

    private async UniTask ChangeSongs(AudioClip clip, float t = 1.5f) {
        if (source.isPlaying) {
            await StopPlaying(t);
        }

        await StartPlaying(clip, t);
    }
}
