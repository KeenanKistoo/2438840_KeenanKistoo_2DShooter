using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyBehave
{
   Wait,
   Search,
   Loiter,
   Chase,
   Freeze
   
}
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

   [SerializeField] private Collider2D[] nearbyColl;

   [SerializeField] private EnemyBehave _enemyBehave;

   [SerializeField] private float timer;

   private void Start()
   {
      _activate = false;
      _enemyBehave = EnemyBehave.Wait;
      timer = 0;
   }

   private void Update()
   {
      switch (_enemyBehave)
      {
         case EnemyBehave.Wait:
            print("Waiting");
            StartCoroutine(Stall());
            break;
         case EnemyBehave.Search:
            print("Searching");
            SelectNode();
            break;
         case EnemyBehave.Loiter:
            Timer();
            MoveTowardsNode();
            print("Loitering");
            break;
         case EnemyBehave.Chase:
            print("Chase Player");
            //ChasePlayer
            break;
         case EnemyBehave.Freeze:
            print("Frozen");
            //Player Does Not Move
            break;
      }
   }

   public void GetEnvironment(Dictionary<Vector2Int, bool> floorPos)
   {
      environment = floorPos;
   }

   private void MoveTowardsNode()
   {
      transform.position = Vector2.Lerp(transform.position, 
         (Vector2)targetPos, (Time.deltaTime * timeVariable));
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

      nearbyColl = Physics2D.OverlapCircleAll(transform.position, 3f);

      for (int i = 0; i < nearbyColl.Length; i++)
      {
         if (nearbyColl[i].tag == "Player")
         {
            _enemyBehave = EnemyBehave.Chase;
            print("Chase");
         }
         else
         {
            _enemyBehave = EnemyBehave.Loiter;
         }
      }

      timer = 0;
   }

   public void Timer()
   {
      timer += Time.deltaTime;

      if (timer >= 4)
      {
         _enemyBehave = EnemyBehave.Search;
      }
   }

   private IEnumerator Stall()
   {
      yield return new WaitForSeconds(5f);

      _enemyBehave = EnemyBehave.Search;
   }
}
