using System;
using UnityEngine;

namespace Lore {
    [CreateAssetMenu(fileName = "Chapter", menuName = "Lore/Chapter", order = 0)]
    public class Chapter : ScriptableObject {
        [SerializeField] private new String name;
        public String Name => name;
    }
}
