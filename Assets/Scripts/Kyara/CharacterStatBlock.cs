using System;

namespace Kyara {
    [Serializable]
    public struct CharacterStatBlock {
        public int health;
        public int magic;
        public int speed;
        public int attack;
        public int defense;
        public int luck;
        public int requiredXP;
    }
}
