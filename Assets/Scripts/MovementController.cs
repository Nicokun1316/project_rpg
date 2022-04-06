using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 1f;

    private ContactFilter2D contactFilter;
    private Rigidbody2D body;
    private List<RaycastHit2D> raycastResults = new();
    private SpriteRenderer sprite;
    
    private Vector2 movementVector = Vector2.zero;
    private List<Vector2> gizmos = new();
    void Start() {
        Application.targetFrameRate = 60;
        Debug.Log("Starting!");
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update() {
        DoMovement(new Vector2(movementVector.x, 0));
        DoMovement(new Vector2(0, movementVector.y));
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        foreach (var gizmo in gizmos) {
            Gizmos.DrawSphere(gizmo, 1);
        }
    }

    void DoMovement(Vector2 moveVector) {
        /* if (hit.collider != null) {
            Debug.DrawLine(transform.position, hit.point);
            Debug.Log($"Hit: ({hit.point.x}, {hit.point.y})");
        } */

        var move = moveVector * Time.deltaTime * speed;
        
        
        var hitCount = body.Cast(move, contactFilter, raycastResults, move.magnitude);
        gizmos.Clear();
        if (hitCount > 0) {
            move = Vector2.zero;
        }
        for (int i = 0; i < hitCount; ++i) {
            var rc = raycastResults[i];
            
            gizmos.Add(rc.point);
            
        }

        // for some reason there is a LOT of jitter with MovePosition so we'll just modify the transform directly
        // var body = GetComponent<Rigidbody2D>();
        // body.MovePosition((Vector2)transform.position + movementVector * Time.deltaTime * speed);
        
        transform.position += (Vector3) move;
    }

    public void Move(InputAction.CallbackContext context) {
        movementVector = context.ReadValue<Vector2>();
    }
}
