using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1f)]
    private float roomPercent = 0.8f;

    [SerializeField] private LightGenerator _lightGenerator;
    protected override void RunProceduralGeneration()
    {
        CorridorFirstDungeonGeneration();
    }

    private void CorridorFirstDungeonGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

        // Generate rooms in valid potential room positions
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        // Find dead ends and create rooms at those positions
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        // Expand corridors while ensuring room positions remain valid
        for (int i = 0; i < corridors.Count; i++)
        {
            List<Vector2Int> expandedCorridor = IncreaseCorridorBrushBy3(corridors[i]);
            floorPositions.UnionWith(expandedCorridor);
        }

        // Combine floor positions from rooms and corridors
        floorPositions.UnionWith(roomPositions);

        // Visualize the dungeon layout
        _tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, _tilemapVisualizer);

        _lightGenerator.LightGeneration(floorPositions);
    }


    private List<Vector2Int> IncreaseCorridorBrushBy3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 0; i < corridor.Count; i++)
        {
            Vector2Int currentPos = corridor[i];
        
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    Vector2Int newPos = currentPos + new Vector2Int(x, y);
                    if (!newCorridor.Contains(newPos))
                    {
                        newCorridor.Add(newPos);
                    }
                }
            }
        }

        return newCorridor;
    }

    public List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previewDirection = Vector2Int.zero;
        for (int i = 0; i < corridor.Count; i++)
        {
            Vector2Int directionFromCell = corridor[i] - corridor[i - 1];
            if (previewDirection != Vector2Int.zero && directionFromCell != previewDirection)
            {
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
                Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
                newCorridor.Add(corridor[i-1]);
                newCorridor.Add(corridor[i-1] + newCorridorTileOffset);
            }
        }

        return newCorridor;
    }

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

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> room)
    {
        foreach (var position in deadEnds)
        {
            if (!room.Contains(position))
            {
                var roomFloor = RunRandomWalk(_randomWalkSo, position);
                room.UnionWith(roomFloor);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                    neighboursCount++;
                
            }
            if(neighboursCount == 1)
                deadEnds.Add(position);
        }

        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomToCreate =
            potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take((roomToCreateCount)).ToList();

        foreach (var roomPosition in roomToCreate)
        {
            var roomFloor = RunRandomWalk(_randomWalkSo, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions,
        HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = PCGAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }

        return corridors;
    }
}
