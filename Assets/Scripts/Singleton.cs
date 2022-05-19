using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Singleton : MonoBehaviour {
    private void Awake() {
        assignInstance();
    }

    private void OnEnable() {
        assignInstance();
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode mode) {
        Initialize();
    }

    private void assignInstance() {
        if (instance == null) {
            SceneManager.sceneLoaded += onSceneLoaded;
            instance = this;
            Initialize();
            DontDestroyOnLoad(instance); 
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    protected abstract Singleton instance { get; set; }
    protected abstract void Initialize();
}