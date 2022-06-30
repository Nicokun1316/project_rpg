using System;
using DevLocker.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lore {
    [CreateAssetMenu(fileName = "Map", menuName = "Lore/Map", order = 0)]
    public class Map : ScriptableObject {
        [SerializeField] private new String name;
        [SerializeField] private SceneReference scene;
        public String Name => name;
        public SceneReference Scene => scene;
    }
}
