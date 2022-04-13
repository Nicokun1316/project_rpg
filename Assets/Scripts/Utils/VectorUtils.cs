using UnityEngine;

namespace Utils {
    public static class VectorUtils {
        public static void Deconstruct(this Vector2 vec, out float x, out float y) {
            x = vec.x;
            y = vec.y;
        }
        
        public static void Deconstruct(this Vector2? vec, out float x, out float y) {
            x = vec?.x ?? 0;
            y = vec?.y ?? 0;
        }
    }
}
