using System;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
