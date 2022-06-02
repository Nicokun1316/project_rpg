using System;

namespace Skills {
    [Serializable]
    public struct SkillEffect {
        public SkillEffectType type;
        public DamageType damageType;
        public SkillTarget validTarget;
        public AoeMode aoeMode;
        public TargetMode targetMode;
        public AbilityStrength strength;
    }
}
