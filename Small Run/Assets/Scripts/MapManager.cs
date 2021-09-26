using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapManager : MonoBehaviour
{
    class TileInfo
    {

        public TileBase tileBase;
        public TileData tileData;

        public Vector3 position;
        public string name;
        public bool isWalkable, isPlacable, isOccupied;
    }

    [SerializeField]private Tilemap tileMap;

    [SerializeField] private List<TileData> tileDatas;
    [SerializeField] private GameObject mouseHoverSquare;

    private Dictionary<TileBase, TileData> dataFromTiles;


    private GameObject mousepos;

    public GameObject selectedGameobject;

    bool DisableGhost = true;

    private void Awake()
    {
        initializeTiles();
        
    }

    private void Start()
    {
        mousepos = Instantiate(mouseHoverSquare, new Vector3(10, 5, 0), Quaternion.identity);
    }


    void Update()
    {


        if (selectedGameobject == null) disableGhost();
        
        

        if(selectedGameobject != null)
        {
            enableGhost();
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int gridPosition = tileMap.WorldToCell(mousePosition);

            TileBase clickedTile = tileMap.GetTile(gridPosition);
            if (dataFromTiles[clickedTile].isPlaceable)
            
            showGhost();
        }

        

    }

    void initializeTiles()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        //list
        foreach (var tileData in tileDatas)
        {
            dataFromTiles.Add(tileData.tile, tileData);
        }

        List<TileInfo> tileInfo;
        foreach (var tiledata in dataFromTiles)
        {
            TileInfo tmp = new TileInfo();
            tmp.name = tiledata.Key.name;
            tmp.isPlacable = tiledata.Value.isPlaceable;
            tmp.isWalkable = tiledata.Value.isWalkable;
            tmp.isOccupied = false;
            
        }
    }

    void showGhost()
    {
        if (selectedGameobject != null && !DisableGhost)
        {
            mousepos.GetComponent<SpriteRenderer>().sprite = selectedGameobject.GetComponent<CharacterInfo>().Ghost;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int gridPosition = tileMap.WorldToCell(mousePosition);
            mousepos.transform.position = new Vector3(gridPosition.x + (mouseHoverSquare.transform.localScale.x / 2), gridPosition.y + (mouseHoverSquare.transform.localScale.y / 2), gridPosition.z);
        }
    }

    public void SpawnCharacter()
    {
        if (selectedGameobject != null)
        {
            GameObject SpawnedItem = Instantiate(selectedGameobject, new Vector3(0, 0, 0), Quaternion.identity);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int gridPosition = tileMap.WorldToCell(mousePosition);
            SpawnedItem.transform.position = new Vector3(gridPosition.x + (SpawnedItem.transform.localScale.x / 2), gridPosition.y + (SpawnedItem.transform.localScale.y / 2), gridPosition.z);
            SpawnedItem.name = selectedGameobject.name;
            
        }
    }

    public void disableGhost()
    {
        DisableGhost = true;
    }

    public void enableGhost()
    {
        DisableGhost = false;
    }
}
