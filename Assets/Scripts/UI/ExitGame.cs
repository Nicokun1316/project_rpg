using UnityEngine;

namespace UI {
    public class ExitGame : MonoBehaviour, ActionButton
    {
    

        public void PerformAction() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
