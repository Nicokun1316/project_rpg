using System;

namespace Skills {
    [Serializable]
    public struct Cost {
        public int value;
        public CostType type;
        public CostScaling scaling;
    }
}
