using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundThreshold : MonoBehaviour {
    [SerializeField] private AudioClip thresholdMusic;
    [SerializeField] private bool returns;
    private bool isPlaying;
    private AudioClip oldClip;

    private void OnTriggerEnter2D(Collider2D col) {
        if (isPlaying && returns) {
            AudioManager.INSTANCE.PlayMusic(oldClip);
            isPlaying = false;
        } else {
            oldClip = AudioManager.INSTANCE.CurrentClip;
            AudioManager.INSTANCE.PlayMusic(thresholdMusic);
            isPlaying = true;
        }
    }
}
