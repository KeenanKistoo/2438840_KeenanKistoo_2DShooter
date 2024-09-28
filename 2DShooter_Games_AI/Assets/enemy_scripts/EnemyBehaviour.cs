using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Transforms:")] 
    [SerializeField]
    private Vector3 _enemyPos;

    [Header("Nodes:")] 
    [SerializeField] private GameObject[] nodeObjects;
    [SerializeField] private bool _nodeSetUp;

    [SerializeField]
    private Collider2D[] _nearbyNodes;

    [SerializeField] private float _nodeRadius;
    
    [SerializeField] private List<Collider2D> nodes = new List<Collider2D>();
    private void Start()
    {
        _enemyPos = this.gameObject.transform.position;
        _nodeSetUp = true;
    }

    private void Update()
    {
        if (_nodeSetUp)
        {
            NodeSetUp();
        }
        
        //MoveTowardsNode();
        
    }

    //Node Setup
    private void NodeSetUp()
    {
        _nodeSetUp = true;
        int nodeCount = GameObject.FindGameObjectsWithTag("Nodes").Length;
        nodeObjects = GameObject.FindGameObjectsWithTag("Nodes");
        _nearbyNodes = Physics2D.OverlapCircleAll(transform.position, _nodeRadius);

        StartCoroutine(Stall());
    }

    
    //Behaviour 1: Aimlessly walking from node to node
    private void MoveTowardsNode()
    {
        int nodeIndex = Random.Range(0, _nearbyNodes.Length);

        if (_nearbyNodes[nodeIndex].tag == "Nodes")
        {
            Transform nodePos = _nearbyNodes[nodeIndex].transform;
            transform.position = Vector2.MoveTowards(transform.position, nodePos.position, Time.deltaTime * 1f);
        }
    }
    
    //Behaviour 2: Chasing the player
    
    //Behaviour 3: Stuck in place because of light


    private IEnumerator Stall()
    {
        print("StoppCheck");
        yield return new WaitForSeconds(8f);
        _nodeSetUp = false;
        MoveTowardsNode();
    }
}
