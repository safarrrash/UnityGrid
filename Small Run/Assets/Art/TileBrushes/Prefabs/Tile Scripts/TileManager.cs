using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] TileSO tileSO;
    bool Occupied;

    public bool isOccupied()
    {
        return Occupied;
    }

    public void isOccupied(bool isOccupied)
    {
        Occupied = isOccupied;
    }

    public bool isPlacable()
    {
        return tileSO.isPlaceable;
    }
}
