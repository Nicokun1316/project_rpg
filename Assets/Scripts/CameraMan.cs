using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour {
    private Rigidbody2D player;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
