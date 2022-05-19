using System;
using UnityEngine;

namespace Items {
    public class Item : ScriptableObject {
        public String itemName;
        public String description;
        public int cost;
        public int id;
        public Texture2D icon;
    }
}
