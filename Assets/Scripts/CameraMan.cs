using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

public class CameraMan : MonoBehaviour {
    private Rigidbody2D player;

    private Vector2 cameraBounds;

    private Bounds sceneBounds;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        var pixelCamera = GetComponent<PixelPerfectCamera>();
        cameraBounds = new Vector2((float) pixelCamera.refResolutionX / pixelCamera.assetsPPU / 2,
            (float) pixelCamera.refResolutionY / pixelCamera.assetsPPU / 2);

        var background = GameObject.FindWithTag("Background").GetComponent<Tilemap>();
        sceneBounds = background.localBounds;
    }

    // Update is called once per frame
    void Update() {
        var x = Math.Clamp(player.position.x, sceneBounds.min.x + cameraBounds.x, sceneBounds.max.x - cameraBounds.x);
        var y = Math.Clamp(player.position.y, sceneBounds.min.y + cameraBounds.y, sceneBounds.max.y - cameraBounds.y);
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
