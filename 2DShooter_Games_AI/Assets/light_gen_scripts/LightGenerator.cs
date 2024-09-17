using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightGenerator : MonoBehaviour
{
    [Header("Light Tracker")] 
    public int lightTracker = 5;
    public int[] setLightValues;

    [SerializeField] private GameObject _torchPrefab;
    [SerializeField] private GameObject _torchParent;

    private void Start()
    {
        _torchParent = GameObject.FindGameObjectWithTag("TorchParent");
    }

    /*public void LightGeneration(HashSet<Vector2Int> _floorPositions)
    {
        
        foreach (var position in _floorPositions)
        {
            int ran = Random.Range(0, 20);
            lightTracker = _floorPositions.Count;
            if (lightTracker > 0)
            {
                for (int i = 0; i < setLightValues.Length; i++)
                {
                    if (ran == setLightValues[i])
                    {
                        GameObject torch = Instantiate(_torchPrefab, _torchParent.transform, false);
                        torch.transform.position = new Vector3(position.x, position.y, 0);
                        //print(position);
                        lightTracker--;
                        //print(ran);
                    }
                }
            }else if (lightTracker <= 0)
            {
                //print("Complete");
            }
        }
    }*/
    
    public void LightGeneration(HashSet<Vector2Int> _floorPositions)
    {
        
        foreach (var position in _floorPositions)
        {
            int ran = Random.Range(0, 20);
            lightTracker = _floorPositions.Count;
            if (lightTracker > 0)
            {
                for (int i = 0; i < setLightValues.Length; i++)
                {
                    
                        GameObject torch = Instantiate(_torchPrefab, _torchParent.transform, false);
                        torch.transform.position = new Vector3(position.x, position.y, 0);
                        //print(position);
                        lightTracker--;
                        //print(ran);
                    
                }
            }else if (lightTracker <= 0)
            {
                //print("Complete");
            }
        }
    }
    
}
