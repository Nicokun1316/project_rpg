using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using Unity.VisualScripting;
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
                if (currentSelection != null) {
                    currentSelection.deselect();
                }

                _index = MathUtils.mod(value, children.Count);
                currentSelection.select();
            }
        }

        void OnEnable() {
            children = transform.GetComponentsInDirectChildren<UIMenuItem>();
            // _index = 0;
            currentSelection.select();
        }

        public void Reset() {
            children = transform.GetComponentsInDirectChildren<UIMenuItem>();
            currentSelection.select();
        }

        public UIMenuItem currentSelection => index < children.Count ? children[index] : null;

        public UIMenuItem[] items => children.ToArray();

        public void StopAnimation() {
            currentSelection.StopAnimation();
        }

        public void ResumeAnimation() {
            currentSelection.ResumeAnimation();
        }
    }
}
