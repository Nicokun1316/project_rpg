using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI {
    public class DPad : MonoBehaviour {
        [SerializeField] public InputManager inputManager;

        [SerializeField] public Orientation direction;
        
        public void mouseDown() {
            inputManager.SendMovementInput(direction.toVector(), "Move");
        }

        public void mouseUp() {
            inputManager.SendMovementInput(Vector2.zero, "Move");
        }
    }
}
