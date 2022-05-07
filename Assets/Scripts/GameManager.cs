using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private MovementController playerController;
    private GameObject player;
    private Rigidbody2D playerBody;
    private ContactFilter2D findFilter;
    private List<RaycastHit2D> raycastResults = new();
    private Vector2? pp;
    private Vector2? pv;
    public GameManager() {
        currentGameState = GameState.WORLD;
        findFilter.useLayerMask = true;
    }
    public static GameManager INSTANCE { get; private set; }

    public static int PPU = 32;

    public static Vector2 PixelClamp(Vector2 vector) {
        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(vector.x * PPU),
            Mathf.RoundToInt(vector.y * PPU));
        return vectorInPixels / PPU;
    }

    private void Initialise() {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<MovementController>();
        playerBody = player.GetComponent<Rigidbody2D>();
    }

    public GameState currentGameState { get; private set; }
    // Start is called before the first frame update
    private void Awake() {
        INSTANCE ??= this;
        INSTANCE.Initialise();
        DontDestroyOnLoad(INSTANCE);
    }

    private void Update() {
        if (pp != null && pv != null) {
            Debug.DrawLine(pp.Value, pv.Value);
        }
    }

    public void TransitionGameState(GameState newState) {
        currentGameState = newState;
    }
    
    public GameObject FindObjectInFrontOfPlayer(int mask) {
        raycastResults.Clear();
        findFilter.layerMask = mask;
        var direction = playerController.orientation switch {
            Orientation.Up => new Vector2(0, 1),
            Orientation.Down => new Vector2(0, -1),
            Orientation.Left => new Vector2(-1, 0),
            Orientation.Right => new Vector2(1, 0),
            _ => throw new ArgumentOutOfRangeException()
        };
        var cr = Physics2D.Raycast(playerBody.position, direction, findFilter, raycastResults, 1.1f);
        //var cr = playerBody.Cast(direction, findFilter, raycastResults, 1.1f);
        pp = playerBody.position;
        pv = playerBody.position + direction;
        //Debug.DrawLine(playerBody.position, playerBody.position + direction, Color.red);
        
        return cr == 0 ? null : raycastResults.First().collider.gameObject;
    }
}
