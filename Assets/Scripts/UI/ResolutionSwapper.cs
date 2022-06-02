using System;
using UnityEngine;

namespace UI {
    public class ResolutionSwapper : MonoBehaviour {
        private RadioGroup radio;
        // Start is called before the first frame update
        void Start() {
            radio = GetComponent<RadioGroup>();
            var items = radio.choice.menuItems;
            if (!PlayerPrefs.HasKey("Resolution")) {
                PlayerPrefs.SetString("Resolution", "1");
            }
            var prefValue = PlayerPrefs.GetString("Resolution");
            var index = Array.FindIndex(items, item => item.GetComponent<ValueHolder>().value == prefValue);
            radio.choice.index = index;
        
            radio.onSelectionChanged += selection => {
                var value = selection.GetComponent<ValueHolder>().value;
                PlayerPrefs.SetString("Resolution", value);
                PlayerPrefs.Save();
                switch (value) {
                    default:
                        Screen.SetResolution(640, 480, false);
                        break;
                    case "2":
                        Screen.SetResolution(1280, 960, false);
                        break;
                    case "fullscreen":
                        var monitor = Screen.mainWindowDisplayInfo;
                        var w = monitor.width;
                        var h = monitor.height;
                        Screen.SetResolution(w, h, true);
                        break;
                }
            };
        }

    }
}
