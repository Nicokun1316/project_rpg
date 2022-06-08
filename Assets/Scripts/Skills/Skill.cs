using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "Ability", menuName = "Character/Skill", order = 0)]
    public class Skill : ScriptableObject {
        public int id;
        public String skillName;
        [TextArea] public String description;
        public Texture2D icon;
        public List<Cost> costs;
        public List<SkillEffect> effects;
        
    }
}
