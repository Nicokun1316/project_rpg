using System;
using System.Collections.Generic;
using DevLocker.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utils {
    public static class GameObjectUtils {
        public static GameObject parent(this GameObject obj) => obj.transform.parent.gameObject;

        public static GameObject FindRecursive(this GameObject obj, String name) {
            return obj.transform.FindRecursive(name).gameObject;
        }

        public static Transform FindRecursive(this Transform trans, String name) {
            foreach (Transform child in trans) {
                if (child.name == name) {
                    return child;
                } else {
                    var result = child.FindRecursive(name);
                    if (result != null) return result;
                }
            }

            return null;
        }

        public static List<T> GetComponentsInDirectChildren<T>(this Transform obj) {
            var components = new List<T>();
            foreach (Transform trans in obj) {
                var c = trans.GetComponent<T>();
                if (c != null) {
                    components.Add(c);
                }
            }

            return components;
        }

        public static AsyncOperation Load(this SceneReference scene) {
            return SceneManager.LoadSceneAsync(scene.ScenePath);
        }

        public static UnityEngine.Color WithAlpha(this UnityEngine.Color color, float alpha) =>
            new(color.r, color.g, color.b, alpha);
    }
}
