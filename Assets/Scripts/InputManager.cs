using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    private MovementController player;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(InputAction.CallbackContext context) {
        player.Move(context.ReadValue<Vector2>());
    }
}
