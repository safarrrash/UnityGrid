using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Ray ray;
    RaycastHit2D hit;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit)
            {
                GameObject hitGO = hit.collider.gameObject;
                print(hitGO.GetComponent<TileManager>().isPlacable());
            }
        }
    }

}
