using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    
    [SerializeField] 
    protected SimpleRandomWalkSO _randomWalkSo;
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(_randomWalkSo, startPosition);

        foreach (var postion in floorPositions)
        {
            Debug.Log((postion));
        }
        _tilemapVisualizer.Clear();
        _tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, _tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO randomWalkSo, Vector2Int position)
    {
        var currentPosition = position;

        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < randomWalkSo.iterations; i++)
        {
            var path = PCGAlgorithms.SimpleRandomWalk(currentPosition, randomWalkSo.walkLength);
            floorPositions.UnionWith(path);
            if (randomWalkSo.startRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }

        return floorPositions;
    }

   
}
