using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioManager : Singleton {
    private AudioSource source;
    protected override Singleton instance { get => INSTANCE; set => INSTANCE = (UIAudioManager) value; }
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
    
    public static UIAudioManager INSTANCE { get; private set; }
}
