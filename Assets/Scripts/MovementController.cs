using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class MovementController : MonoBehaviour
{
    public Orientation orientation { private set; get; }

    private Rigidbody2D body;
    private Vector2 virtualPosition;

    [SerializeField] private float speed = 1;
    [SerializeField] private List<Tilemap> obstacleMaps;
    private bool moving = false;
    public bool isMoving => moving;
    
    void Start() {
        body = GetComponent<Rigidbody2D>();
        virtualPosition = body.position;
        orientation = Orientation.Up;
    }

    public async UniTask MoveCharacter(Vector2 offset, Orientation orientation) {
        if (moving) return;
        this.orientation = orientation;
        var d = acquireDestination(offset);
        if (d == null) return;
        
        moving = true;
        var destination = d.Value;
        
        while (virtualPosition != destination) {
            await UniTask.WaitForFixedUpdate();
            if (GameManager.INSTANCE.currentGameState != GameState.WORLD) {
                continue;
            }

            var delta = speed * Time.fixedDeltaTime;
            var distance = Vector2.Distance(destination, virtualPosition);
            var maxOffset = Vector2.ClampMagnitude(offset * delta, distance);
            var newPosition = virtualPosition + maxOffset;
            virtualPosition = newPosition;
            body.position = GameManager.PixelClamp(newPosition);
        }

        moving = false;
    }

    public async UniTask MoveCharacter(Vector2 offset) {
        await MoveCharacter(offset, orientationFor(offset));
    }

    private Orientation orientationFor(Vector2 moveVector) => moveVector switch {
        (0, 1) => Orientation.Up,
        (0, -1) => Orientation.Down,
        (1, 0) => Orientation.Right,
        (-1, 0) => Orientation.Left,
        _ => orientation
    };

    private Vector2? acquireDestination(Vector2 offset) {
        if (offset == Vector2.zero) return null;

        var destination = virtualPosition + offset;
        var tilePosition = Vector3Int.FloorToInt(destination);
        var isObstructed = obstacleMaps.Any(m => m.HasTile(tilePosition));
        return isObstructed ? null : destination;
    }
}
