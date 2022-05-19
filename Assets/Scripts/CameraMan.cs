using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMan : MonoBehaviour {
    private Rigidbody2D player;

    private Tilemap background;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        background = GameObject.FindWithTag("Background").GetComponent<Tilemap>();
        var camera = GetComponent<Camera>();
        
        var bounds = background.localBounds;
        //bounds.
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
