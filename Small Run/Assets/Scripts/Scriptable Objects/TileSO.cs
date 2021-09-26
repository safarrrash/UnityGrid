using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Custom/Tile")]

public class TileSO : ScriptableObject
{
    public Sprite TileBase;
    public bool isWalkable;
    public bool isPlaceable;

}
