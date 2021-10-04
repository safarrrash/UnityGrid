using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI DebugText;
    
    Ray ray;
    RaycastHit2D hit;

    public GameObject WhiteSquare;
    [SerializeField] GameObject SpawnedCharactersParent;

    [SerializeField] GameObject Pawn;
    [SerializeField] GameObject Blob;

    private bool PlacingMode;
    [SerializeField] private GameObject defaultTile;

    GameObject[] tilesA;
    GameObject[] PlaceableTiles;


    private void Start()
    {
        /*tilesA = GameObject.FindGameObjectsWithTag("Tile");
        print(tilesA.Length.ToString());
        int k = 0;
        for (int i = 0; i < tilesA.Length; i++)
        {
            if (tilesA[i].GetComponent<TileManager>().isPlacable())
            {
                
                PlaceableTiles[k]= tilesA[i].gameObject;
                
                k++;
            }
        }

        DebugText.text = tilesA.Length.ToString();
        */
    }

    void Update()
    {
        if (isInPlacingMode()) enterPlacingMode();

        if (Input.GetKeyDown(KeyCode.O)) togglePlacingMode(true);
        if (Input.GetKeyDown(KeyCode.K)) togglePlacingMode(false);

        if (isInPlacingMode() && Input.GetMouseButtonDown(0))
        {
            spawnCharacter(Pawn, GetHitGameObject().transform.position);
        }
        if (isInPlacingMode() && Input.GetMouseButtonDown(1))
        {
            spawnCharacterAny(Blob, GetHitGameObject().transform.position);
        }

    }

    void enterPlacingMode()
    {
        //highlightPlaceAbleTiles(true);

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
        int layermask = (LayerMask.GetMask("Tile"));
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layermask);
        if (!hit)
        {

            return defaultTile;

        }
        else
        {
            return hit.collider.gameObject;
        }
    }

    void highlightPlaceAbleTiles(bool on)
    {

        Color tent = PlaceableTiles[0].GetComponent<SpriteRenderer>().color + new Color(10, 10, 10);
        Color defaultColor = PlaceableTiles[0].GetComponent<SpriteRenderer>().color;
        if (on)
        {
            foreach(GameObject tile in PlaceableTiles) {
                tile.GetComponent<SpriteRenderer>().color = tent;
            }
        }
        
        else if (!on)
        {
            foreach (GameObject tile in PlaceableTiles)
            {
                tile.GetComponent<SpriteRenderer>().color = defaultColor;
            }
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
            GameObject spawnedCharacter = Instantiate(character, position, Quaternion.identity, SpawnedCharactersParent.transform);
            
        }
    }

    void spawnCharacterAny(GameObject character, Vector3 position)
    {
        if (GetHitGameObject().tag == "Tile")
        {
            GameObject spawnedCharacter = Instantiate(character, position, Quaternion.identity, SpawnedCharactersParent.transform);
        }
    }
}
