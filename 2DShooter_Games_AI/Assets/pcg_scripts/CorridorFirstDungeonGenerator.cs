using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    // Corridor length and count to determine dungeon generation parameters
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;

    // Percentage of potential rooms that will actually be created
    [SerializeField]
    [Range(0.1f,1f)]
    private float roomPercent = 0.8f;

    // Reference to the LightGenerator script that will handle lighting generation
    [SerializeField] private LightGenerator _lightGenerator;
    
    // List of Room Dimensions
    [SerializeField] private List<Room> rooms = new List<Room>();

    [SerializeField] private GameObject _chestPrefab;

    [SerializeField] private Tilemap _floorTilemap;


    // Main entry point for procedural generation - calls the dungeon generation method
    protected override void RunProceduralGeneration()
    {
        CorridorFirstDungeonGeneration();
    }

    private void CorridorFirstDungeonGeneration()
    {
        // Stores floor tile positions
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        // Stores positions that could potentially be rooms
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        // Create corridors and populate floor positions and potential room positions
        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

        // Generate rooms based on the potential room positions
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        // Identify dead-end tiles and turn them into rooms
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        // Expand corridors by increasing their width while maintaining room positions
        for (int i = 0; i < corridors.Count; i++)
        {
            List<Vector2Int> expandedCorridor = IncreaseCorridorBrushBy3(corridors[i]);
            floorPositions.UnionWith(expandedCorridor); // Adds expanded corridor to the floor positions
        }

        // Merge the floor positions from rooms and corridors
        floorPositions.UnionWith(roomPositions);

        // Visualize the dungeon floor tiles
        _tilemapVisualizer.PaintFloorTiles(floorPositions);
        // Generate walls around the floor tiles
        WallGenerator.CreateWalls(floorPositions, _tilemapVisualizer);

        //Place my chests
        PlaceChest();
        
        //Place Torches
        _lightGenerator.LightGeneration(potentialRoomPositions);
    }

    // Expands the corridor size by 3x3 around each point of the original corridor
    private List<Vector2Int> IncreaseCorridorBrushBy3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 0; i < corridor.Count; i++)
        {
            Vector2Int currentPos = corridor[i];
        
            // Add surrounding positions around the current corridor tile
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    Vector2Int newPos = currentPos + new Vector2Int(x, y);
                    if (!newCorridor.Contains(newPos)) // Avoid duplicates
                    {
                        newCorridor.Add(newPos);
                    }
                }
            }
        }

        return newCorridor;
    }

    // Alternate method to increase corridor size by one tile
    /*public List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previewDirection = Vector2Int.zero;

        // Iterate through each corridor tile
        for (int i = 1; i < corridor.Count; i++) // Start from 1 to avoid out-of-range error
        {
            Vector2Int directionFromCell = corridor[i] - corridor[i - 1];
            if (previewDirection != Vector2Int.zero && directionFromCell != previewDirection)
            {
                // If the direction changes, expand the surrounding tiles around the corner
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i-1] + new Vector2Int(x,y));
                    }
                }

                previewDirection = directionFromCell;
            }
            else
            {
                // Add a new corridor tile offset to make it wider
                Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
                newCorridor.Add(corridor[i-1]);
                newCorridor.Add(corridor[i-1] + newCorridorTileOffset);
            }
        }

        return newCorridor;
    }*/

    // Get a direction perpendicular to the given direction (90-degree turn)
    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
            return Vector2Int.right;
        if (direction == Vector2Int.right)
            return Vector2Int.down;
        if (direction == Vector2Int.down)
            return Vector2Int.left;
        if (direction == Vector2Int.left)
            return Vector2Int.up;
        return Vector2Int.zero;
    }

    // Create rooms at dead-end positions by running a random walk algorithm
    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> room)
    {
        foreach (var position in deadEnds)
        {
            if (!room.Contains(position)) // Only create a room if it's not already a room
            {
                var roomFloor = RunRandomWalk(_randomWalkSo, position); // Generate room floor
                room.UnionWith(roomFloor); // Add room to the room positions
            }
        }
    }

    // Find all dead-end tiles by checking how many neighbors each floor tile has
    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            // Count how many neighboring floor tiles the current tile has
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                    neighboursCount++;
            }
            // If the tile has only one neighbor, it's a dead end
            if(neighboursCount == 1)
                deadEnds.Add(position);
        }

        return deadEnds;
    }

    // Create rooms at a subset of potential room positions
    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        // Calculate how many rooms to create based on the roomPercent
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        // Shuffle potential room positions and take a subset
        List<Vector2Int> roomToCreate =
            potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take((roomToCreateCount)).ToList();

        // Generate floor tiles for each selected room position
        foreach (var roomPosition in roomToCreate)
        {
            var roomFloor = RunRandomWalk(_randomWalkSo, roomPosition); // Generate room floor using random walk
            roomPositions.UnionWith(roomFloor); // Add room floor to room positions
            
            // Create the Room instance
            int minX = roomFloor.Min(pos => pos.x);
            int maxX = roomFloor.Max(pos => pos.x);
            int minY = roomFloor.Min(pos => pos.y);
            int maxY = roomFloor.Max(pos => pos.y);
            Room newRoom = new Room(new Vector2Int(minX, minY), new Vector2Int(maxX, maxY));

            // Add the new room to the list of rooms
            rooms.Add(newRoom);
        }

        return roomPositions;
    }

    // Create corridors between random points and fill in floor tiles along the way
    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions,
        HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition; // Starting point for the first corridor
        potentialRoomPositions.Add(currentPosition); // Mark start as potential room position
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        // Generate corridors for the specified number of corridorCount
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = PCGAlgorithms.RandomWalkCorridor(currentPosition, corridorLength); // Generate corridor
            corridors.Add(corridor); // Add corridor to list
            currentPosition = corridor[corridor.Count - 1]; // Update current position to the end of the corridor
            potentialRoomPositions.Add(currentPosition); // Mark end as potential room position
            floorPositions.UnionWith(corridor); // Add corridor to floor positions
        }

        return corridors;
    }
    
    //Place Chests In Room
    private void PlaceChest()
    {
        foreach (Room room in rooms)
        {
            // Get a random position inside the room
            Vector2Int chestPosition = new Vector2Int(
                UnityEngine.Random.Range(room.bottomLeftCorner.x, room.topRightCorner.x + 1), // Ensure topRightCorner.x is included
                UnityEngine.Random.Range(room.bottomLeftCorner.y, room.topRightCorner.y + 1)  // Ensure topRightCorner.y is included
            );
        
            // Place the chest at pos
            print("Placing chest at: " + chestPosition);
            GameObject chest = Instantiate(_chestPrefab, new Vector3(chestPosition.x, chestPosition.y, 0), Quaternion.identity, null);
            
        }
    }

    //Place Light Posts In Room
    
    //Place Enemies In Room
}



[System.Serializable]
public class Room
{
    [SerializeField] public Vector2Int bottomLeftCorner { get; private set; }
    [SerializeField] public Vector2Int topRightCorner { get; private set; }

    public Room(Vector2Int bottomLeft, Vector2Int topRight)
    {
        this.bottomLeftCorner = bottomLeft;
        this.topRightCorner = topRight;
    }

    public int Width => topRightCorner.x - bottomLeftCorner.x;
    public int Height => topRightCorner.y - bottomLeftCorner.y;
}

