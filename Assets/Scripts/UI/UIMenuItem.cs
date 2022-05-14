using UnityEngine;
using Utils;

namespace UI {
    public class UIMenuItem : MonoBehaviour {
        private GameObject arrowField;

        public GameObject arrow {
            get {
                if (arrowField == null) {
                    arrowField = gameObject.FindRecursive("Arrow");
                }

                return arrowField;
            }
        }

        private Blinky blinkyArrow;
        // Start is called before the first frame update
        void Start() {
            blinkyArrow = arrow.GetComponent<Blinky>();
        }

        public void StopAnimation() {
            blinkyArrow.StopAnimation();
        }

        public void ResumeAnimation() {
            blinkyArrow.StartAnimation();
        }
    }
}
