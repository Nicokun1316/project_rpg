using System;
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
    }
}
