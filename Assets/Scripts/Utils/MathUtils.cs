using System;

namespace Utils {
    public static class MathUtils {
        public static int mod(int a, int b) {
            return a - b * (int) Math.Floor((double) a / b);
        }
    }
}
