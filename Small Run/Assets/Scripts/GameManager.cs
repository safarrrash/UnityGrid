using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Ray ray;
    RaycastHit2D hit;

    public GameObject WhiteSquare;
    [SerializeField] GameObject SpawnedCharactersParent;

    [SerializeField] GameObject Pawn;

    private bool PlacingMode;
    [SerializeField] private GameObject defaultTile;

    void Update()
    {
        if(isInPlacingMode()) enterPlacingMode();

        if (Input.GetKeyDown(KeyCode.O)) togglePlacingMode(true);
        if (Input.GetKeyDown(KeyCode.K)) togglePlacingMode(false);

        if (isInPlacingMode() && Input.GetMouseButtonDown(0))
        { 
            spawnCharacter(Pawn, GetHitGameObject().transform.position);
        }

    }

    void enterPlacingMode()
    {
        if (GetHitGameObject().tag == "Tile")
        {
            if (GetHitGameObject().GetComponent<TileManager>().isPlacable())
            {
                GameObject selectedTile = GetHitGameObject();
                WhiteSquare.transform.position = selectedTile.transform.position;
            }
            else
            {
                WhiteSquare.transform.position = defaultTile.transform.position;
            }
        }
    }

    

    GameObject GetHitGameObject()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (!hit)
        {
           
            return defaultTile;

        }
        else
        {
            return hit.collider.gameObject;
        }
    }

    void togglePlacingMode(bool isEnabled)
    {
        if (isEnabled) PlacingMode = true;
        else if (!isEnabled)
        {
            PlacingMode = false;
            WhiteSquare.transform.position = defaultTile.transform.position;
        }
    }

    bool isInPlacingMode()
    {
        return PlacingMode;
    }

    void spawnCharacter(GameObject character, Vector3 position)
    {
        if(GetHitGameObject().tag == "Tile")
        if (GetHitGameObject().GetComponent<TileManager>().isPlacable())
        {
            GameObject spawnedCharacter = Instantiate(Pawn, position, Quaternion.identity, SpawnedCharactersParent.transform);
            
        }
    }
}
