using System;

namespace Skills {
    [Serializable]
    public struct AbilityStrength {
        public int value;
        public AbilityScaling scalingType;
        public float scaling;
    }
}
