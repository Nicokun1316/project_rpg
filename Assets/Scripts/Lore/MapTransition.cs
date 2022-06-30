using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Lore {
    [RequireComponent(typeof(BoxCollider2D))]
    public class MapTransition : MonoBehaviour {
        [SerializeField] private Map map;

        private void Awake() {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("Player")) {
                GameManager.TransitionMap(map).Forget();
            }
        }
    }
}
