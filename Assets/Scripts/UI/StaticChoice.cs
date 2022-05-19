using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;
using Utils;
using MathUtils = Utils.MathUtils;

namespace UI {
    public class StaticChoice : MonoBehaviour, MenuChoice {
        private List<UIMenuItem> children;

        void OnEnable() {
            children = transform.GetComponentsInDirectChildren<UIMenuItem>();
            currentSelection.select();
        }

        private int _index;

        public int index {
            get => _index;
            set {
                currentSelection.deselect();
                _index = MathUtils.mod(value, children.Count);
                currentSelection.select();
            }
        }

        public UIMenuItem currentSelection => children[index];
        public UIMenuItem[] items => children.ToArray();

        public void StopAnimation() {
            currentSelection.StopAnimation();
        }

        public void ResumeAnimation() {
            currentSelection.ResumeAnimation();
        }
    }
}
