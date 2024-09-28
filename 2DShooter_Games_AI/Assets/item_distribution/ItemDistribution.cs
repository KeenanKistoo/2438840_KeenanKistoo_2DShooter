using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Chest,
    Enemy, 
    Torch,
    Node
}
public class ItemDistribution : MonoBehaviour
{
    [Header("Item Type:")]
    [SerializeField]
    private ItemType _currentItem;

    [Header("Desired Number of Items")]
    public int desChest;
    public int desEnemy;
    public int desTorch;
    public int desNode;

    [Header("Set Up Bool")] 
    public bool _needSetup;

    [Header("Hashset Of Floor Positions:")] 
    public List<Vector2Int> floorPos;

    [Header("Prefabs:")] 
    [SerializeField] private GameObject _chest;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _torch;
    [SerializeField] private GameObject _node;

    [Header("Parent Objects:")] 
    [SerializeField] private GameObject _chestParent;
    [SerializeField] private GameObject _enemyParent;
    [SerializeField] private GameObject _torchParent;
    [SerializeField] private GameObject _nodeParent;
    
    
    private void Start()
    {
        _currentItem = ItemType.Chest;
        _needSetup = false;
        
    }


    private void Update()
    {
        if (_needSetup)
        {
            ItemSetup();
        }
    }

    public void ItemSetup()
    {
        foreach (var pos in floorPos)
        {
            if (desChest <=0 && desEnemy <= 0 && desTorch <= 0 && desNode <= 0)
            {
                Debug.Log("All items placed!");
                _needSetup = false;
                return;  // Stop further processing when all desired items are placed
            }

            int ranNum = UnityEngine.Random.Range(0, 80);
            if (ranNum == 19)
            {
                switch (_currentItem)
                {
                    case ItemType.Chest:
                        if (desChest > 0)
                        {
                            desChest--;
                            GameObject chestInstance = Instantiate(_chest, new Vector3(pos.x, pos.y, 0),
                                Quaternion.identity, _chestParent.transform);
        
                            //print("Added Chest at " + pos);
                        }else if (desChest <= 0)
                        {
                            _currentItem = ItemType.Enemy;
                        }
                        break;
                    case ItemType.Enemy:
                        if (desEnemy > 0)
                        {
                            desEnemy--;
                            GameObject enemyInstance = Instantiate(_enemy, new Vector3(pos.x, pos.y, 0),
                                Quaternion.identity, _enemyParent.transform);
                            //print("Added Enemy at " + pos);
                        }else if (desEnemy <= 0)
                        {
                            _currentItem = ItemType.Torch;
                        }
                        break;
                    case ItemType.Torch:
                        if (desTorch > 0)
                        {
                            desTorch--;
                            GameObject torchInstance = Instantiate(_torch, new Vector3(pos.x, pos.y, 0),
                                Quaternion.identity, _torchParent.transform);
                            //print("Added Torch at " + pos);
                        }else if (desTorch <= 0)
                        {
                            _currentItem = ItemType.Node;
                        }
                        break;
                    case ItemType.Node:
                        if (desNode > 0)
                        {
                            desNode--;
                            GameObject nodeInstance = Instantiate(_node, new Vector3(pos.x, pos.y, 0),
                                Quaternion.identity, _nodeParent.transform);
                            //print("Added Torch at " + pos);
                        }else if (desNode <= 0)
                        {
                            _needSetup = false;
                        }
                        break;
                }
            }
        }
    }
    
}
