using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class MovementController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2? destination = null;
    private Rigidbody2D body;
    private Vector2 inputVector;

    [SerializeField] private float speed = 1;
    [SerializeField] private List<Tilemap> obstacleMaps;
    
    void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        updateDestination();
        
        var delta = speed * Time.deltaTime;
        
        if (destination is not null) {
            var (cx, cy) = body.position;
            var (dx, dy) = destination;
            var (nx, ny) = (cx, dx) switch {
                var (_, _) when cx < dx => (Math.Min(cx + delta, dx), cy),
                var (_, _) when cx > dx => (Math.Max(cx - delta, dx), cy),
                var (_, _) when cy < dy => (cx, Math.Min(cy + delta, dy)),
                var (_, _) when cy > dy => (cx, Math.Max(cy - delta, dy)),
                var (_, _) => (cx, cy)
            };
            
            if (!Mathf.Approximately(nx, dx) || !Mathf.Approximately(ny, dy)) {
                body.position = new Vector2(nx, ny);
            } else {
                destination = null;
            }
        }
    }

    private void updateDestination() {
        if (destination is null && inputVector != Vector2.zero) {
            var (x, y) = inputVector;
            var (cx, cy) = body.position;
            Vector2? dest = null;
            if (x != 0) {
                dest = x > 0 ? new Vector2(cx + 1, cy) : new Vector2(cx - 1, cy);
            } else if (y != 0) {
                dest = y > 0 ? new Vector2(cx, cy + 1) : new Vector2(cx, cy - 1);
            }

            if (dest is not null) {
                var d = Vector3Int.FloorToInt(dest ?? default(Vector2));
                var isObstructed = obstacleMaps.Any(m => m.HasTile(d));
                if (!isObstructed) {
                    destination = dest;
                }
            }
        }
    }

    public void Move(Vector2 moveVec) {
        inputVector = moveVec;
    }
}
