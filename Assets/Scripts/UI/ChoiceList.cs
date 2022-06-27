using System;
using System.Collections.Generic;
using System.Linq;
using Skills;
using UnityEngine;
using Utils;

namespace UI {
    public class ChoiceList<C, T> : MonoBehaviour, MenuChoice where C : IEnumerable<T> {
        private int _index;
        private List<T> _choices;
        private List<UIMenuItem> children;
        private int pageLength => children.Count;
        [SerializeField] private C itemSource;
        UIMenuItem MenuChoice.currentSelectedMenuItem => (items?.Count ?? 0) > 0 ? children[index % pageLength] : null;
        private UIMenuItem csmi => (this as MenuChoice).currentSelectedMenuItem;
        public UIMenuItem[] menuItems => children.Take(_choices.Count).ToArray();

        public int index {
            
            get => _index;
            set {
                if (_choices.Count > 0) {
                    foreach (var menuItem in children) {
                        menuItem.gameObject.SetActive(false);
                    }
                    csmi?.deselect();
                    _index = MathUtils.mod(value, _choices.Count);
                    var startingIndex = _index / pageLength * pageLength;
                    var currentPageLength = Math.Min(_choices.Count - startingIndex, pageLength);
                    for (int i = 0; i < currentPageLength; ++i) {
                        children[i].gameObject.SetActive(true);
                    }
                    
                    for (int i = 0; i < pageLength && startingIndex + i < _choices.Count; ++i) {
                        children[i].GetComponent<GenericItemComponent<T>>().Item = _choices[startingIndex + i];
                    }

                    csmi?.select();
                }
            }
        }

        public List<T> items {
            get => _choices;
            set {
                _choices = value;
                foreach (var menuItem in children) {
                    menuItem.gameObject.SetActive(true);
                }
                
                if (_choices.Count < children.Count) {
                    for (int i = _choices.Count; i < children.Count; ++i) {
                        children[i].gameObject.SetActive(false);
                    }
                }

                index = 0;
            }
        }
        public void Reset() {
        }

        public void StopAnimation() {
            csmi?.StopAnimation();
        }

        public void ResumeAnimation() {
            csmi?.ResumeAnimation();
        }

        private void OnEnable() {
            children = transform.GetComponentsInDirectChildren<UIMenuItem>();
            csmi?.select();
            foreach (var menuItem in children) {
                menuItem.gameObject.SetActive(false);
            }

            items = itemSource.ToList();
        }
    }
}
