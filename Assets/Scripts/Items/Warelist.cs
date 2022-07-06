using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Items {
    [CreateAssetMenu(fileName = "Wares", menuName = "Items/Wares", order = 0)]
    public class Warelist : ScriptableObject, IEnumerable<Item> {
        [SerializeField] private List<Item> stock;
        public ReadOnlyCollection<Item> Stock => stock.AsReadOnly();
        IEnumerator IEnumerable.GetEnumerator() {
            return stock.GetEnumerator();
        }

        public IEnumerator<Item> GetEnumerator() {
            return stock.GetEnumerator();
        }
    }
}
