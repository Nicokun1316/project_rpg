using UnityEngine;

namespace UI {
    public abstract class GenericItemComponent<T> : MonoBehaviour {
        [SerializeField] private T _item;

        public T item {
            get => _item;
            set {
                _item = value;
                InvalidateItem();
            }
        }

        protected abstract void InvalidateItem();
    }
}
