using UnityEngine;

namespace UI {
    public class TabItem : MonoBehaviour, RedirectingFocusable {
        [SerializeField]
        private GameObject targetTab;

        Focusable RedirectingFocusable.target => targetTab.GetComponent<Focusable>();
    }
}
