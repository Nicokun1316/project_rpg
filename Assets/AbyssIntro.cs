using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class AbyssIntro : MonoBehaviour {
    [SerializeField] private GameObject player;

    [SerializeField] private Tilemap bedTilemap;
    [SerializeField] private Vector3Int bedPosition;
    [SerializeField] private List<TileBase> bedTiles;

    private void Start() {
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<Light2D>().enabled = false;
        var im = InputManager.INSTANCE;
        im.RegisterInputHook("Move", _ => {
            player.GetComponent<SpriteRenderer>().enabled = true;
            player.GetComponent<Light2D>().enabled = true;
            im.UnregisterInputHook("Confirm");
            im.UnregisterInputHook("Cancel");
            for (var i = bedPosition; i.x - bedPosition.x < bedTiles.Count; i += Vector3Int.right) {
                var tile = bedTiles[i.x - bedPosition.x];
                bedTilemap.SetTile(i, tile);
            }
            player.GetComponent<MovementController>().Move(Vector2.down);

            return true;
        });
        im.RegisterInputHook("Confirm", _ => false);
        im.RegisterInputHook("Cancel", _ => false);
    }
}
