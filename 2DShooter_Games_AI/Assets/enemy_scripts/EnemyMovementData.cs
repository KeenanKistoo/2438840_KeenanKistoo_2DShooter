using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovementData : MonoBehaviour
{
   public Dictionary<Vector2Int, bool> environment = new Dictionary<Vector2Int, bool>();

   public bool _activate;

   public float timeVariable;

   [SerializeField] 
   private bool isMoving;

   [SerializeField] private bool _startMove;

   [SerializeField] 
   private Vector2 targetPos;

   private void Start()
   {
      _activate = false;
   }

   private void Update()
   {
      if (_activate)
      {
         if (!isMoving)
         {
            SelectNode();
         }else if (isMoving)
         {
            _startMove = true;
         }
      }

      if (_startMove)
      {
         StartCoroutine(WalkPhase());
      }
   }

   public void GetEnvironment(Dictionary<Vector2Int, bool> floorPos)
   {
      environment = floorPos;
   }

   private void MoveTowardsNode()
   {
      
      transform.position = Vector2.Lerp(transform.position, (Vector2)targetPos, (Time.deltaTime * timeVariable));
   }
   private void SelectNode()
   {
      // Get the keys (positions) from the dictionary as a List
      List<Vector2Int> keys = new List<Vector2Int>(environment.Keys);

      // Select a random index
      int index = Random.Range(0, keys.Count);

      // Get the random key (position) from the list
      Vector2Int randomPosition = keys[index];

      // Check if the selected position is walkable (if needed)
      if (environment[randomPosition])
      {
         //print(environment[randomPosition]);
         targetPos = (Vector2)randomPosition;
      }

      isMoving = true;
   }

   private IEnumerator WalkPhase()
   {
      MoveTowardsNode();
      _activate = false;
      yield return new WaitForSeconds(4f);
      _activate = true;
      isMoving = false;
   }
}
