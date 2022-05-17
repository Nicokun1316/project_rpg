using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ResolutionSwapper : MonoBehaviour {
    private RadioGroup radio;
    // Start is called before the first frame update
    void Start() {
        radio = GetComponent<RadioGroup>();
        radio.onSelectionChanged += selection => {
            var value = selection.GetComponent<ValueHolder>().value;
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
