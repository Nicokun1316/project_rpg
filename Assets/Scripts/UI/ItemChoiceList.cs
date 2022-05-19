using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;
using Utils;

namespace UI {
    public class ItemChoiceList : MonoBehaviour, MenuChoice {
        public UIMenuItem[] items => children.Take(_cItems.Count).ToArray();
        private List<UIMenuItem> children;
        private List<Item> _cItems;
        private int pageLength => children.Count;
        [SerializeField] private InventoryList inventory;
        public UIMenuItem currentSelection => children[index % pageLength];

        private void OnEnable() {
            children = transform.GetComponentsInDirectChildren<UIMenuItem>();
            currentSelection.select();
            foreach (var menuItem in children) {
                menuItem.gameObject.SetActive(false);
            }

            cItems = inventory.items;
        }

        public List<Item> cItems {
            get => _cItems;
            set {
                _cItems = value;
                foreach (var menuItem in children) {
                    menuItem.gameObject.SetActive(true);
                }
                if (_cItems.Count < children.Count) {
                    for (int i = _cItems.Count; i < children.Count; ++i) {
                        children[i].gameObject.SetActive(false);
                    }
                }

                index = 0;
            }
        }
        private int _index;

        public int index {
            get => _index;
            set {
                if (_cItems.Count > 0) {
                    foreach (var menuItem in children) {
                        menuItem.gameObject.SetActive(false);
                    }
                    currentSelection.deselect();
                    _index = MathUtils.mod(value, _cItems.Count);
                    var startingIndex = _index / pageLength * pageLength;
                    var currentPageLength = Math.Min(_cItems.Count - startingIndex, pageLength);
                    for (int i = 0; i < currentPageLength; ++i) {
                        children[i].gameObject.SetActive(true);
                    }
                    
                    for (int i = 0; i < pageLength && startingIndex + i < cItems.Count; ++i) {
                        Debug.Log($"i = {i}, chilcount = {children.Count}, startindex = {startingIndex}, itemslen = {_cItems.Count}; itemsindex = {startingIndex + i}");
                        children[i].GetComponent<ItemComponent>().item = _cItems[startingIndex + i];
                    }

                    currentSelection.select();
                }
            }
        }
        
        public void StopAnimation() {
            currentSelection.StopAnimation();
        }

        public void ResumeAnimation() {
            currentSelection.ResumeAnimation();
        }
    }
}
