using System.Collections.Generic;
using UnityEngine;
using Utils;
using MathUtils = Utils.MathUtils;

namespace UI {
    public class StaticChoice : MonoBehaviour, MenuChoice {
        private List<UIMenuItem> children;
        private int _index;
        public int index {
            get => _index;
            set {
                if (currentSelectedMenuItem != null) {
                    currentSelectedMenuItem.deselect();
                }

                _index = MathUtils.mod(value, children.Count);
                currentSelectedMenuItem.select();
            }
        }

        void OnEnable() {
            children = transform.GetComponentsInDirectChildren<UIMenuItem>();
            // _index = 0;
            currentSelectedMenuItem.select();
        }

        public void Reset() {
            children = transform.GetComponentsInDirectChildren<UIMenuItem>();
            currentSelectedMenuItem.select();
        }

        public UIMenuItem currentSelectedMenuItem => index < children.Count ? children[index] : null;

        public UIMenuItem[] menuItems => children.ToArray();

        public void StopAnimation() {
            currentSelectedMenuItem.StopAnimation();
        }

        public void ResumeAnimation() {
            currentSelectedMenuItem.ResumeAnimation();
        }
    }
}
