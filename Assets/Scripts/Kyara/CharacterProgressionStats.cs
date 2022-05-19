using System.Collections.Generic;
using UnityEngine;

namespace Kyara {
    [CreateAssetMenu(fileName = "stat_block", menuName = "Character/Stat block", order = 0)]
    public class CharacterProgressionStats : ScriptableObject {
        public List<CharacterStatBlock> statProgression;
    }
}
