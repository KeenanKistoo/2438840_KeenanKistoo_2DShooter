using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;

    [SerializeField]
    private TileBase floorTile, wallTop;

    [SerializeField] 
    private TileBase wallClean, wallRock;

    public bool rockTypeControl;


    private void Start()
    {
        rockTypeControl = false;
    }

    private void Update()
    {
        if (rockTypeControl)
        {
            int ranNum = Random.Range(0, 25);
            if (ranNum % 5 == 0)
            {
                floorTile = wallRock;
            }
            else
            {
                floorTile = wallClean;
            }
            
        }
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            int randomTileNum = Random.Range(0, 45);

            if (randomTileNum % 10 == 0)
            {
                tile = wallRock;
            }
            else
            {
                tile = wallClean;
            }
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    public void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTop, position);
    }

    public IEnumerator CheckWalls()
    {
        rockTypeControl = true;
        yield return new WaitForSeconds(5f);
        rockTypeControl = false;
    }
}
