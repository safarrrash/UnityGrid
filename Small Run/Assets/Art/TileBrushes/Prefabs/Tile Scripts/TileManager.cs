using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] TileSO tileSO;

    public bool isPlacable()
    {
        return tileSO.isPlaceable;
    }
}
