using UnityEngine;

namespace UI {
    public abstract class GenericItemComponent<T> : MonoBehaviour {
        [SerializeField] private T item;

        public T Item {
            get => item;
            set {
                item = value;
                InvalidateItem();
            }
        }

        protected abstract void InvalidateItem();
    }
}
