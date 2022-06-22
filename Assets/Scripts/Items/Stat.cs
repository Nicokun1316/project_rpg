using System;

namespace Items {
    public struct Stat {
        public readonly StatType name;
        public readonly int value;
        public Stat(StatType name, int value) {
            this.name = name;
            this.value = value;
        }
    }
}
