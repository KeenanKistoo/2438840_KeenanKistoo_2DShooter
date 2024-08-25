using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    
    [SerializeField] 
    private SimpleRandomWalkSO _randomWalkSo;
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();

        foreach (var postion in floorPositions)
        {
            Debug.Log((postion));
        }
        _tilemapVisualizer.Clear();
        _tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, _tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;

        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < _randomWalkSo.iterations; i++)
        {
            var path = PCGAlgorithms.SimpleRandomWalk(currentPosition, _randomWalkSo.walkLength);
            floorPositions.UnionWith(path);
            if (_randomWalkSo.startRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }

        return floorPositions;
    }

   
}
