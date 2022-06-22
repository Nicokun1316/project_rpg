using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Skills;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHotSwapScope : MonoBehaviour {
    [SerializeField] private List<TileBase> baseTiles;
    [SerializeField] private List<TileBase> swapTiles;
    [SerializeField] private Skill activateSkill;
    public ReadOnlyCollection<TileBase> BaseTiles => baseTiles.AsReadOnly();
    public ReadOnlyCollection<TileBase> SwapTiles => swapTiles.AsReadOnly();
    public Skill ActivateSkill => activateSkill;
}
