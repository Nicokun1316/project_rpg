using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIAudioManager : Singleton {
    private AudioSource source;

    protected override Singleton instance {
        get => _INSTANCE;
        set => _INSTANCE = (UIAudioManager) value;
    }

    protected override void Initialize() {
        source = GetComponent<AudioSource>();
    }

    public void Play() {
        source.Play();
    }

    public void Stop() {
        source.Pause();
        source.time = 0;
    }

    private static UIAudioManager _INSTANCE;

    public static UIAudioManager INSTANCE =>
        _INSTANCE ??= new GameObject("UIAudioManager", typeof(AudioSource)).AddComponent<UIAudioManager>();
}
