using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour {
    [field: SerializeField] public Orientation Orientation { private set; get; } = Orientation.Up;
    [SerializeField] private bool pixelClamp = false;

    public Vector2 Direction => Orientation switch {
        Orientation.Up => Vector2.up,
        Orientation.Down => Vector2.down,
        Orientation.Left => Vector2.left,
        Orientation.Right => Vector2.right,
        _ => throw new ArgumentOutOfRangeException()
    };

    private Rigidbody2D body;
    private Vector2 virtualPosition;
    private Animator animator;

    [SerializeField] private float speed = 1;
    private List<Tilemap> obstacleMaps;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Moving = Animator.StringToHash("Moving");
    public bool IsMoving { get; private set; } = false;
    public Vector2 Position => body.position;

    public float Speed {
        get => speed;
        set => speed = value;
    }

    void Start() {
        body = GetComponent<Rigidbody2D>();
        virtualPosition = body.position;
        animator = GetComponent<Animator>();
        obstacleMaps = GameObject.FindGameObjectsWithTag("ObstaclesBase")
            .Select(g => g.GetComponent<Tilemap>())
            .Where(t => t is not null)
            .ToList();
        UpdateAnimator();
    }

    public async UniTask MoveCharacter(Vector2 offset, Orientation orientation) {
        if (IsMoving) return;
        offset = FixDirection(offset);
        Turn(orientation);
        var d = AcquireDestination(offset);
        if (d == null) return;
        
        IsMoving = true;
        var destination = d.Value;
        
        if (animator != null)
            animator.SetBool(Moving, true);

        while (virtualPosition != destination) {
            await UniTask.WaitForFixedUpdate();
            if (GameManager.INSTANCE.CurrentGameState != GameState.WORLD) {
                continue;
            }

            var delta = speed * Time.fixedDeltaTime;
            var distance = Vector2.Distance(destination, virtualPosition);
            var maxOffset = Vector2.ClampMagnitude(offset * delta, distance);
            var newPosition = virtualPosition + maxOffset;
            virtualPosition = newPosition;
            body.position = pixelClamp ? GameManager.PixelClamp(newPosition) : newPosition; // GameManager.PixelClamp(newPosition);
        }
        
        if (animator != null)
            animator.SetBool(Moving, false);

        IsMoving = false;
    }

    private Vector2 FixDirection(Vector2 offset) {
        if (offset == Vector2.zero) {
            return offset;
        } else if (Math.Abs(offset.x) > Math.Abs(offset.y)) {
            return new Vector2(offset.x > 0 ? 1 : -1, 0);
        } else {
            return new Vector2(0, offset.y > 0 ? 1 : -1);
        }
    }

    public async UniTask MoveCharacter(Vector2 offset) {
        await MoveCharacter(offset, OrientationFor(offset));
    }

    public void Turn(Orientation orientation) {
        Orientation = orientation;
        UpdateAnimator();
    }

    private void UpdateAnimator() {
        var moveVec = Orientation switch {
            Orientation.Down => Vector2.down,
            Orientation.Up => Vector2.up,
            Orientation.Left => Vector2.left,
            Orientation.Right => Vector2.right,
            _ => throw new ArgumentOutOfRangeException()
        };

        var (x, y) = moveVec;
        if (animator != null) {
            animator.SetFloat(Horizontal, x);
            animator.SetFloat(Vertical, y);
            animator.speed = speed / 4;
        }

    }

    private Orientation OrientationFor(Vector2 moveVector) => moveVector switch {
        (0, 1) => Orientation.Up,
        (0, -1) => Orientation.Down,
        (1, 0) => Orientation.Right,
        (-1, 0) => Orientation.Left,
        _ => Orientation
    };

    private Vector2? AcquireDestination(Vector2 offset) {
        if (offset == Vector2.zero) return null;

        var destination = virtualPosition + offset;
        var tilePosition = Vector3Int.FloorToInt(destination);
        var isObstructed = obstacleMaps.Any(m => m.HasTile(tilePosition)) ||
                           GameManager.INSTANCE.FindObjectInFrontAt(LayerMask.GetMask("Interactible"), 1.0f) != null;
        return isObstructed ? null : destination;
    }
}
