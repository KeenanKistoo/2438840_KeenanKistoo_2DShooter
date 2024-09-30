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

   [SerializeField] private bool _isChasing;

   [Header("Audio Elements")] 
   public AudioSource _audioSource;
   public AudioClip _chased;
   public AudioClip _caught1;
   public AudioClip _caught2;
   public AudioClip _win;
   public AudioClip _lose;
   

   private void Start()
   {
      _activate = false;
      _isChasing = false;
      _enemyBehave = EnemyBehave.Wait;
      timer = 0;
      //_audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
   }

   private void Update()
   {
      switch (_enemyBehave)
      {
         case EnemyBehave.Wait:
            //print("Waiting");
            StartCoroutine(Stall());
            break;
         case EnemyBehave.Search:
            //print("Searching");
            SelectNode();
            break;
         case EnemyBehave.Loiter:
            if (!_isChasing)
            {
               Timer(4f);
               MoveTowardsNode();
               //print("Loitering");
            }else if (_isChasing)
            {
               _enemyBehave = EnemyBehave.Chase;
            }
            break;
         case EnemyBehave.Chase:
            //print("Chase Player");
            
            ChasePlayer();
            //ChasePlayer
            break;
         case EnemyBehave.Freeze: 
            Timer(2f);
            print("Frozen For Time");
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
         // Ensure constant movement towards the target
         transform.position = Vector2.MoveTowards(transform.position, targetPos,
            timeVariable * Time.deltaTime);
   }
   private void SelectNode()
   {
      nearbyColl = Physics2D.OverlapCircleAll(transform.position, 3f);

      for (int i = 0; i < nearbyColl.Length; i++)
      {
         //print(nearbyColl[i].tag);
         if (nearbyColl[i].tag == "Player")
         {
            _isChasing = true;
            _enemyBehave = EnemyBehave.Chase;
            //print("Initiate Chase");
         }
         else
         {
            _enemyBehave = EnemyBehave.Loiter;
         }
      }
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

      timer = 0;
   }

   private void Timer(float stopTime){
    
      timer += Time.deltaTime;

      if (timer >= stopTime)
      {
         _enemyBehave = EnemyBehave.Search;
      }
   }

   private IEnumerator Stall()
   {
      yield return new WaitForSeconds(5f);

      _enemyBehave = EnemyBehave.Search;
   }

   private void ChasePlayer()
   {
      targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;
      _audioSource = this.gameObject.GetComponent<AudioSource>();
      _audioSource.clip = _chased;
      _audioSource.Stop();
      _audioSource.Play(); 
      //StartCoroutine(BreathSound());
      MoveTowardsNode();
   }

   private IEnumerator BreathSound()
   {
      _audioSource.clip = _chased;
      _audioSource.Stop();
      print("Clip:" + _audioSource.clip);

      yield return new WaitForSeconds(0.5f);
      
      _audioSource.Play();
   }

   public void Freeze()
   {
      timer = 0;
      _enemyBehave = EnemyBehave.Freeze;
      print("Frozen");
   }
}
