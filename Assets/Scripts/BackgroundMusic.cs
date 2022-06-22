using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {
    [SerializeField] private AudioClip music;
    private void Awake() {
        AudioManager.INSTANCE.PlayMusic(music);
    }
}
