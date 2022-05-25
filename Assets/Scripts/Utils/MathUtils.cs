using System;
using System.Collections.Generic;

namespace Utils {
    public static class MathUtils {
        public static int mod(int a, int b) {
            return a - b * (int) Math.Floor((double) a / b);
        }

        public static V GetOrDefault<K, V>(this Dictionary<K, V> dict, K key, V def) {
            return dict.TryGetValue(key, out var val) ? val : def;
        }
    }
}
