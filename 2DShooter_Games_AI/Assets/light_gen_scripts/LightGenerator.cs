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
    [SerializeField] private GameObject _messagePanel;

    private void Start()
    {
        _torchParent = GameObject.FindGameObjectWithTag("TorchParent");
        _messagePanel = GameObject.FindWithTag("MessagePanel");
        StartCoroutine(Stall());
    }
    
    public void LightGeneration(HashSet<Vector2Int> _floorPositions)
    {
        lightTracker = _floorPositions.Count;
        foreach (var position in _floorPositions)
        {
            
            print(lightTracker);
            if (lightTracker > 0)
            {
                for (int i = 0; i < setLightValues.Length; i++)
                {
                        lightTracker--;
                        GameObject torch = Instantiate(_torchPrefab, _torchParent.transform, false);
                        torch.transform.position = new Vector3(position.x, position.y, 0);
                        //print(position);
                        //print("Track No: " + lightTracker);
                        //print(ran);
                    
                }
            }else if (lightTracker <= 0)
            {
                //print("Complete");
                _messagePanel.SetActive(false);
                break;
            }
        }
    }

    private IEnumerator Stall()
    {
        yield return new WaitForSeconds(5f);

        if (_messagePanel.activeInHierarchy)
        {
            _messagePanel.SetActive(false);
        }
    }
    
}
