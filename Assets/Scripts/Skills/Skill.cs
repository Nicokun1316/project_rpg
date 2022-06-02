using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "Ability", menuName = "Character/Skill", order = 0)]
    public class Skill : ScriptableObject {
        public String skillName;
        [TextArea] public String description;
        public List<Cost> costs;
        public List<SkillEffect> effects;
        public Texture2D icon;
        public int id;
    }
}
