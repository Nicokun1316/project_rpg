using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    [Serializable]
    public class TextSequence {
        [TextArea]
        public List<String> lines;
    }
}
