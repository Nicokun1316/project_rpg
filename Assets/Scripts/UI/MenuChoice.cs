using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using MathUtils = Utils.MathUtils;

namespace UI {
    public class MenuChoice : MonoBehaviour {
        private List<UIMenuItem> children;

        private int index;
    
        void OnEnable() {
            children = transform.GetComponentsInDirectChildren<UIMenuItem>();
            currentSelection.select();
        }

        public UIMenuItem currentSelection => children[index];
        public UIMenuItem[] items => children.ToArray();

        public void Next() {
            SetIndex(index + 1);
        }

        public void Previous() {
            SetIndex(index - 1);
        }

        public void SetIndex(int i) {
            currentSelection.deselect();
            index = MathUtils.mod(i, children.Count);
            currentSelection.select();
        }

        public void StopAnimation() {
            currentSelection.StopAnimation();
        }

        public void ResumeAnimation() {
            currentSelection.ResumeAnimation();
        }
    }
}
