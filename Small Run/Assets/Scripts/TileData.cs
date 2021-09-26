using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Tile", menuName = "New/Tile")]

public class TileData : ScriptableObject
{
    public TileBase tile;

    public bool isWalkable, isPlaceable;

}
