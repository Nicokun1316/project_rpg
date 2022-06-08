using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "skills", menuName = "Character/Skill list", order = 0)]
    public class SkillList : ScriptableObject, IEnumerable<Skill> {
        public List<Skill> knownSkills;
        public IEnumerator<Skill> GetEnumerator() {
            return knownSkills.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return knownSkills.GetEnumerator();
        }

        private void OnEnable() {
            knownSkills = new List<Skill>();
        }
    }
}
