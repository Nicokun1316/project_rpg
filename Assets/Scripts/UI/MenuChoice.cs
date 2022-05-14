using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MathUtils = Utils.MathUtils;

namespace UI {
    public class MenuChoice : MonoBehaviour {
        private List<UIMenuItem> children;

        private int index;
    
        void OnEnable() {
            children = transform.GetComponentsInChildren<UIMenuItem>(true).ToList();
            currentSelection.arrow.SetActive(true);
        }

        public UIMenuItem currentSelection => children[index];

        public void Next() {
            currentSelection.arrow.SetActive(false);
            index = MathUtils.mod(index + 1, children.Count);
            currentSelection.arrow.SetActive(true);
        }

        public void Previous() {
            currentSelection.arrow.SetActive(false);
            index = MathUtils.mod(index - 1, children.Count);
            currentSelection.arrow.SetActive(true);
        }

        public void SetIndex(int i) {
            currentSelection.arrow.SetActive(false);
            index = Math.Clamp(i, 0, children.Count - 1);
            currentSelection.arrow.SetActive(true);
        }

        public void StopAnimation() {
            currentSelection.StopAnimation();
        }

        public void ResumeAnimation() {
            currentSelection.ResumeAnimation();
        }
    }
}
