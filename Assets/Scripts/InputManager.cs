using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    private GameObject player;

    private Rigidbody2D pb;

    private MovementController mc;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player");
        pb = player.GetComponent<Rigidbody2D>();
        mc = player.GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(InputAction.CallbackContext context) {
        mc.Move(context.ReadValue<Vector2>());
    }

    public void Confirm(InputAction.CallbackContext context) {
        //pb.Cast();
    }
}
