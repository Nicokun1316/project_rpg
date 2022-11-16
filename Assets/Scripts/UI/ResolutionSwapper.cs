using System;
using UnityEngine;

namespace UI {
    public class ResolutionSwapper : MonoBehaviour {
        private RadioGroup radio;
        // Start is called before the first frame update
        void Start() {
            radio = GetComponent<RadioGroup>();
            var items = radio.Choice.menuItems;
            if (!PlayerPrefs.HasKey("Resolution")) {
                PlayerPrefs.SetString("Resolution", "1");
            }
            var prefValue = PlayerPrefs.GetString("Resolution");
            var index = Array.FindIndex(items, item => item.GetComponent<ValueHolder>().value == prefValue);
            radio.Choice.index = index;
        
            radio.onSelectionChanged += selection => {
                var value = selection.GetComponent<ValueHolder>().value;
                PlayerPrefs.SetString("Resolution", value);
                PlayerPrefs.Save();
                GameManager.SetResolution(value);
            };
        }

    }
}
