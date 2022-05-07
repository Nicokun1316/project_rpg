using UnityEngine;

namespace Utils {
    public static class GameObjectUtils {
        public static GameObject parent(this GameObject obj) => obj.transform.parent.gameObject;
    }
}
