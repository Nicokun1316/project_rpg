using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Singleton : MonoBehaviour {
    private void Awake() {
        AssignInstance();
    }

    private void OnEnable() {
        AssignInstance();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Initialize();
    }

    private void AssignInstance() {
        if (instance == null) {
            SceneManager.sceneLoaded += OnSceneLoaded;
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