using System;

namespace Items {
    public struct Stat {
        public readonly String name;
        public readonly int value;
        public Stat(String name, int value) {
            this.name = name;
            this.value = value;
        }
    }
}
