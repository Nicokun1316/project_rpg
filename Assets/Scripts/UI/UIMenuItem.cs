using System;
using TMPro;
using UnityEngine;
using Utils;

namespace UI {
    public class UIMenuItem : MonoBehaviour {
        private GameObject arrowField;
        [SerializeField] private bool hasArrow = true;
        private Blinky blinkyArrow;
        private TMP_Text tmpText;

        public String text {
            get {
                if (tmpText == null) {
                    tmpText = gameObject.FindRecursive("Text").GetComponent<TMP_Text>();
                }

                return tmpText.text;
            }

            set {
                if (tmpText == null) {
                    tmpText = gameObject.FindRecursive("Text").GetComponent<TMP_Text>();
                }

                tmpText.text = value;
            }
        }

        public GameObject arrow {
            get {
                if (!hasArrow) return null;
                if (arrowField == null) {
                    arrowField = gameObject.FindRecursive("Arrow");
                }

                return arrowField;
            }
        }

        public void select() {
            arrow?.SetActive(true);
            GetComponent<Focusable>()?.Unfreeze();
        }

        public void deselect() {
            arrow?.SetActive(false);
            GetComponent<Focusable>()?.Freeze();
        }

        // Start is called before the first frame update
        void Start() {
            blinkyArrow = arrow?.GetComponent<Blinky>();
        }

        public void StopAnimation() {
            blinkyArrow?.StopAnimation();
        }

        public void ResumeAnimation() {
            blinkyArrow?.StartAnimation();
        }
    }
}
