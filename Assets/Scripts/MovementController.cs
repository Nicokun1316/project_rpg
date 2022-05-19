using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class MovementController : MonoBehaviour
{
    public Orientation orientation { private set; get; }
    
    // Start is called before the first frame update
    private Vector2? destination = null;
    private Rigidbody2D body;
    private Vector2 inputVector;
    private Vector2 virtualPosition;

    [SerializeField] private float speed = 1;
    [SerializeField] private List<Tilemap> obstacleMaps;
    
    void Start() {
        body = GetComponent<Rigidbody2D>();
        virtualPosition = body.position;
        orientation = Orientation.Up;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (GameManager.INSTANCE.currentGameState != GameState.WORLD) {
            return;
        }
        updateDestination();
        
        var delta = speed * Time.fixedDeltaTime;
        
        if (destination is not null) {
            var (cx, cy) = virtualPosition;
            var (dx, dy) = destination.Value;
            var (nx, ny) = (cx, dx) switch {
                var (_, _) when cx < dx => (Math.Min(cx + delta, dx), cy),
                var (_, _) when cx > dx => (Math.Max(cx - delta, dx), cy),
                var (_, _) when cy < dy => (cx, Math.Min(cy + delta, dy)),
                var (_, _) when cy > dy => (cx, Math.Max(cy - delta, dy)),
                var (_, _) => (cx, cy)
            };
            
            virtualPosition = new Vector2(nx, ny);
            body.position = GameManager.PixelClamp(virtualPosition);

            
            if (Mathf.Approximately(nx, dx) && Mathf.Approximately(ny, dy)) {
                destination = null;
            }
        }
    }

    private void updateDestination() {
        if (destination is null && inputVector != Vector2.zero) {
            orientation = inputVector switch {
                (0, 1) => Orientation.Up,
                (0, -1) => Orientation.Down,
                (1, 0) => Orientation.Right,
                (-1, 0) => Orientation.Left,
                _ => orientation
            };
            var (x, y) = inputVector;
            var (cx, cy) = virtualPosition;
            Vector2? dest = null;
            if (x != 0) {
                dest = x > 0 ? new Vector2(cx + 1, cy) : new Vector2(cx - 1, cy);
            } else if (y != 0) {
                dest = y > 0 ? new Vector2(cx, cy + 1) : new Vector2(cx, cy - 1);
            }

            if (dest is not null) {
                var d = Vector3Int.FloorToInt((Vector3) dest);
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
